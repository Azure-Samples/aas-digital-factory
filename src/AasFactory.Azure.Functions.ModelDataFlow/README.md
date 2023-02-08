# Model Data Flow Functions <!-- omit in toc -->

This project includes the Azure Functions that will be part of Model Data Flow.

## Sections <!-- omit in toc -->

- [Model Data](#model-data)
  - [AAS Model Data Changed Function](#aas-model-data-changed-function)
    - [AAS Model Data Changed Event](#aas-model-data-changed-event)
    - [AAS Data Changed Payload](#aas-data-changed-payload)
- [Configure](#configure)
- [Error Handling](#error-handling)
- [Permissions](#permissions)
- [Run the Azure Functions locally](#run-the-azure-functions-locally)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
- [Test](#test)

## Model Data

### AAS Model Data Changed Function

This function acts on an Event Hub message containing the path to the AAS model data and the workspace id.
The function will read the file from storage and write the data to ADT (Azure Digital Twins) using the ADT SDK.

#### AAS Model Data Changed Event

```json
{
    "Path": "<path to some aas json file>",
    "Workspace": "sample123"
}
```

- **Path**: The path to the file in blob storage. Path should not include the container name in the prefix.
- **Workspace**: The id of the pipeline that triggered the function.

#### AAS Data Changed Payload

The json file in blob storage is expected to conform to the definition of the message from [this design doc](../../docs/model-data-raw-to-aas.md).

## Configure

The following configuration parameters may be defined...

- **AAS_EVENT_HUB_NAME** [REQUIRED]: The Event Hub where the AAS model update events will be sent.

- **ADT_INSTANCE_URL** [REQUIRED]: The url of the ADT instance (ex: `https://{instanceName}.{location}.digitaltwins.azure.net`).

- **APPLICATIONINSIGHTS_CONNECTION_STRING** [OPTIONAL]: The application insights connection string.

- **AzureWebJobsStorage** [REQUIRED]: The connection string for a storage account that will contain operational data for the Azure Functions.
  This can also reference the blob emulator if running locally.

- **AZURE_TENANT_ID** [OPTIONAL]: The tenant id of the Azure Directory where ADT instance is part of

- **FUNCTIONS_WORKER_RUNTIME** [REQUIRED]: This should be set to "dotnet".

- **EVENT_HUB_CONNECTION_STRING** [REQUIRED]: The connection string of the Event Hub namespace used.
Note that when running without managed identity,
the full connection string in the format `"Endpoint=sb://<your-event-hub-namespace>.servicebus.windows.net/;SharedAccessKeyName=...;SharedAccessKey=..."`
is required.

However, when using managed identity (as ours is when deployed), the setting should have the `__fullyQualifiedNamespace` postfix (i.e. `EVENT_HUB_CONNECTION_STRING__fullyQualifiedNamespace`),
in the format of just the hostname, `<your-event-hub-namespace>.servicebus.windows.net`.
In this case, the managed identity trying to connect to the Event Hub would also need Reader and Sender permissions for it.
For more information, see the [Authenticate the Client section of the Event Hubs extension package page](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.EventHubs/5.0.0-beta.7#readme-body-tab).

- **STORAGE_ACCOUNT_CONNECTION_STRING** [REQUIRED]: The connection string for the storage account.

- **STORAGE_ACCOUNT_CONTAINER_NAME** [REQUIRED]: The container that will host all the AAS model data.

- **CONTINUE_ON_ADT_ERRORS** [REQUIRED]: This flag is used by ADT client for error handling. By default its set to true.

## Error Handling

**ADT Error Handling:**

ADT SDK throws `RequestFailedException` for any return status codes of 400 or above when there is an issue
with the twins/relationships requests submitted. There will be scenarios where the function has to fail fast,
and for some scenarios
failing fast can be made optional via a configuration setting.
The failing fast configuration setting will be made available for only certain statuses as outlined below.
Below is how the error handling is done for the ADT Create/Replace/Delete operations for the model update flow.

- **Create/Replace Twins/Relationships:**
Error log will contain event id 501 for twins and 505 for relationships with associated error message, error code,
status code and twin id/relationship id.

  - `400 (Bad Request), 404 (Not found)` – Error details will be logged and function execution will continue
  if `CONTINUE_ON_ADT_ERRORS` (configuration setting) is set to `true`, else code will throw exception to exit
  the function.
  - `All other errors` - Error details will be logged and code will throw the exception and exit the function.

- **Delete Twins/Relationships:**
Error log will contain event id 502 for twins and 504 for relationships with associated error message, error code,
status code and twin id/relationship id.

  - `400(Bad Request, happens for twins only when there are still relationships associated with the twins)` –
  Error details will be logged and function execution will continue if `CONTINUE_ON_ADT_ERRORS`
  (configuration setting) is set to `true` else code will throw exception to exit the function.
  - `404(Not found)` - Error details will be logged and function execution will continue.
  - `All other errors` - Error details will be logged and code will throw the exception and exit the function.

- **Query Twins/Relationships:**
Error log will contain event id 513 for twins and 514 for relationships with associated error message, error code,
status code and twin id/relationship id.

  - `All errors` - Error details will be logged and code will throw the exception and exit the function.

## Permissions

When running this solution locally, the dev running the solution will need to log in using the [azure cli](https://learn.microsoft.com/en-us/cli/azure/).
The following roles will need to be established as well (for either the dev or the function app depending on how this solution is run).

- **Azure Digital Twins**: Digital Twins Data Owner

## Run the Azure Functions locally

### Prerequisites

- [Azure Function core tools v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash)

  ```bash
    npm install -g azure-functions-core-tools@4
  ```

- If running in [VSCode](https://code.visualstudio.com/), install the follwoing extensions:
  - [Azure Functions](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
  - [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

### Steps

To copy start with the sample configuration:

```bash
cp sample.local.settings.json local.settings.json
```

To run the Function go to the function project and run the command:

```bash
func start
```

Note that if you would like to use the provided configurations and the VSCode UI to launch and/or debug the project,
you will need to open a new window at the level of this project's `.csproj` file to get those to load properly due to the multi-root workspace setup.

## Test

To run unit tests go to the test project and run the command:

```bash
dotnet test
```

To run a coverage report:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

To view the coverage report, you must install the report generator:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

And then you can generate the report:

```bash
# use the guid returned in TestResults path. The html coverage report will be generated under the coveragereport directory.
reportgenerator "-reports:TestResults\{guid}\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```
