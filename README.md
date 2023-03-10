# AAS Factory Azure Integrations <!-- omit in toc -->

- [How to use the sample](#how-to-use-the-sample)
  - [Prerequisites](#prerequisites)
    - [Software pre-requisites](#software-pre-requisites)
  - [Setup and Deployment](#setup-and-deployment)
- [More Links](#more-links)
- [Trademarks](#trademarks)

This repository contains all required components to integrate the services with Azure Event hubs and Azure Digital Twins.

The folder structure for this repo is as below:

- docs: where all technical documentation of the project located in.
- src: contains all source code for the projects.
  - AasFactory.Azure.Functions:
  a folder with all implemented Azure functions and their required components such as services, repositories, interfaces and exceptions.
  - AasFactory.Azure.Models:
  containing all AAS models and other shared models that will be used across Azure functions and services.
  It also contains all event hub contracts and any other components that encapsulating a specific logic to easily work with EventHub.
  - AasFactory.Services: contains any utility services shared across multiple Functions, e.g. a service to interact with the Azure Blob Storage SDK.
- tools:
any tools and components that may be used during the development (such as event hub event producer)

## How to use the sample

### Prerequisites

1. [Azure Account](https://azure.microsoft.com/en-us/free/)

     - Permissions needed: ability to create and deploy to an azure resource group.

#### Software pre-requisites

- Bash Terminal
- [.Net 6.0](https://dotnet.microsoft.com/en-us/download)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Azure Functions Core Tools v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio) (Optional and only used for local development)
  - After installation, it needs to be manually started using Azurite commands
- All required extensions will be recommended to be installed when opening the workspace (check extensions.json file in .vscode folder).

### Setup and Deployment

> IMPORTANT NOTE 1: Please **do not** copy this example and use it in production! This solution is not production ready.

> IMPORTANT NOTE 2: As with all Azure Deployments, this will incur associated costs. Remember to teardown all related resources after use to avoid unnecessary costs.
> This deployment was tested using Git Bash on Windows.

1. **Pull down the repository and cd into the root**

1. **Azure CLI Setup**

   - Ensure that:
     - You are logged in to the Azure CLI. To login, run

         ```bash
         az login --tenant <AZURE_TENANT_ID>
         ```

     - Azure CLI is targeting the Azure Subscription you want to deploy the resources to. To set target Azure Subscription, run

         ```bash
         az account set -s <AZURE_SUBSCRIPTION_ID>
         ```

1. **Setup local.settings.json**

   - The Model Data Flow and Telemetry Data Flow Function Apps will need to have a `local.settings.json` file established prior to executing the deployment step.
      This file only needs to contain the `FUNCTIONS_WORKER_RUNTIME` for the purposes of this setup.
      If running locally, the `local.settings.json` file will have to abide by the necessary environment variables defined in the README of each project.

   - The snippet below is an example of the minimum requirement for the `local.settings.json` file in the Model Data Flow and Telemetry Data Flow Function Apps.

      ```json
      {
        "IsEncrypted": false,
        "Values": {
          "FUNCTIONS_WORKER_RUNTIME": "dotnet"
        }
      }
      ```

1. **Deploy Azure Resources**

   - Run `./deploy.sh`

     - This script take in a few paramters

         ```bash
         ./deploy.sh \
           -c <abbreviated_company_name> \
           -p <resource_prefix> \
           -l westus3
         ```

     - This may take around **20 minutes**. This script deploys the necessary resources, the azure function code, and the initial sample blob.
       - Ensure there aren't errors post running the script.
          If there are any errors, please attempt to fix the error(s) and rerun the deploy script.
          If the attempt to fix the error does not work, please submit an issue.

1. **Trigger Model Data Flow**

   > Before triggering Model Data Flow, ensure the file `Factory.json` exists in the `{prefix}dev{location}sa` storage account.
   > The prefix and location in the storage account name are the prefix and location used in the deploy step above.
   > The file should exist in the `model-data-container` container.
   > If using a custom factory definition, make sure to upload the file to the `model-data-container` container within the `{prefix}dev{location}sa` storage account.
   > Ensure the contents of `FactoryModelDataChanged.sample.json` reflect the path to this new file within the storage account.

   - cd into `./tools/AasFactory.EventHubSimulator`
   - Follow the steps in [the README](./tools/AasFactory.EventHubSimulator/README.md)
   - In the end, the `local.settings.json` file should look something similar to the following

      ```json
      {
          "EVENT_HUB_CONNECTION_STRING": "<event_hub_connection_string>",
          "EVENT_DETAILS_FILE": "./Events/FactoryModelDataChanged.sample.json",
          "EVENT_TYPE": "1"
      }
      ```

   - Run the program with `dotnet run`. This will trigger the model data flow azure functions from the `Deploying Azure Resources` step.
   - After running the simulator, the corresponding graph should appear in the Azure Digital Twins (ADT) instance provisioned in the deploy step.
      For `Factory.json`, Model Data Flow should finish in about 5 minutes.
      Larger factories will take a longer amount of time.
      To view the graph in ADT graph, open the twin explorer and select `Run Query`.
      For this first render of the graph, leave the query as `SELECT * FROM digitaltwins`.

1. **Trigger Streaming Data Flow**

    > For testing purposes Streaming data can be triggered by using the `AasFactory.EventHubSimulator`.

     - cd into `./tools/AasFactory.StreamingSimulator`
     - Follow the steps in [the README](./tools/AasFactory.StreamingSimulator/README.md)
     - In the end, the `local.settings.json` file should look something similar to the following

       ```json
       {
         "EVENT_HUB_CONNECTION_STRING": "<event_hub_connection_string>",
         "EVENT_HUB_NAME": "factory-telemetry-data-changed-eh",
         "BLOB_STORAGE_CONNECTION_STRING": "<blob_storage_connection_string>",
         "BLOB_STORAGE_CONTAINER": "model-data-container",
         "BLOB_STORAGE_BLOB_PATH": "Factory.json",
         "TIME_BETWEEN_EVENTS_IN_SECONDS": 30
       }
       ```

     - Run the program with `dotnet run`. This will trigger the streaming data flow azure functions from the `Deploying Azure Resources` step.
     - You should now see values for the properties of the machine instance on the graph.

## More Links

- The project uses [central package management feature](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management).
All C# projects that need to install a nuget package should specify the version of the nuget package in [Directory.Packages.props](./Directory.Packages.props).

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft trademarks or logos is subject to and must follow Microsoft???s Trademark & Brand Guidelines. Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship. Any use of third-party trademarks or logos are subject to those third-party???s policies.
