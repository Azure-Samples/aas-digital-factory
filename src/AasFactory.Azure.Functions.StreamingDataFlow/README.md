# Streaming Data Functions <!-- omit in toc -->

This project includes the Azure Functions that will be part of Streaming Data Flow.

## Sections <!-- omit in toc -->

- [Configure](#configure)
- [Error Handling](#error-handling)
- [Permissions](#permissions)
- [Run the Azure Functions locally](#run-the-azure-functions-locally)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)

## Configure

The following configuration parameters may be defined...

- **AAS_EVENT_HUB_NAME** [REQUIRED]: The Event Hub where the AAS streaming update events will be sent.

- **ADT_INSTANCE_URL** [REQUIRED]: The url of the ADT instance (ex: `https://{instanceName}.{location}.digitaltwins.azure.net`).

- **APPLICATIONINSIGHTS_CONNECTION_STRING** [REQUIRED]: The connection string to the Application Insights instance used for logging and monitoring.

- **AzureWebJobsStorage** [REQUIRED]: The connection string for a storage account that will contain operational data for the Azure Functions.
  This can also reference the blob emulator if running locally.

- **AZURE_TENANT_ID** [OPTIONAL]: The tenant id of the Azure Directory where ADT instance is part of

- **CIRCUIT_BREAKER_ALLOWED_EXCEPTION_COUNT** [DEFAULT: 3]: The number of consecutive exceptions allowed before the circuit breaks for ADT SDK calls.

- **CIRCUIT_BREAKER_WAIT_TIME_SEC** [DEFAULT: 60]: The duration of the circuit break (in seconds) after the number of max allowed exceptions is reached.

- **COUNTER_CACHE_EXPIRY_IN_SECONDS** [DEFAULT: 30]: The counter cache expiry in seconds.
  This value represents how long the cache will be locked after a cache hit.

- **EVENT_HUB_CONNECTION_STRING** [REQUIRED]: The connection string of the Event Hub namespace used.
Note that when running without managed identity,
the full connection string in the format `"Endpoint=sb://<your-event-hub-namespace>.servicebus.windows.net/;SharedAccessKeyName=...;SharedAccessKey=..."`
is required.

However, when using managed identity (as ours is when deployed), the setting should have the `__fullyQualifiedNamespace` postfix (i.e. `EVENT_HUB_CONNECTION_STRING__fullyQualifiedNamespace`),
in the format of just the hostname, `<your-event-hub-namespace>.servicebus.windows.net`.
In this case, the managed identity trying to connect to the Event Hub would also need Reader and Sender permissions for it.
For more information, see the [Authenticate the Client section of the Event Hubs extension package page](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.EventHubs/5.0.0-beta.7#readme-body-tab).

- **FUNCTIONS_WORKER_RUNTIME** [REQUIRED]: This should be set to "dotnet".

## Error Handling

**ADT Error Handling:**

ADT SDK throws `RequestFailedException` for any return status codes of 400 or above when there is an issue
with the twins update requests. Expectation for telemetry flow is to proceed processing other updates
even when one update fails.

- **Update Twins:**
Error log will contain event id 503 for twins with associated error message, error code,
status code and twin id.
  - `All errors` -  Error details will be logged and function execution will continue.

## Permissions

When running this solution locally, the dev running the solution will need to log in using the [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/).
The following roles will need to be established as well (for either the dev or the function app depending on how this solution is run).

- **Azure Digital Twins**: Digital Twins Data Owner

## Run the Azure Functions locally

### Prerequisites

- [Azure Function core tools v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash)

  ```bash
    npm install -g azure-functions-core-tools@4
  ```

- If running in [VSCode](https://code.visualstudio.com/), install the following extensions:
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
