# AAS Factory Azure Integrations

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
- tests:
containing all projects required for testing including unit-testing, component testing, integration testing, performance and end-2-end testing.
All unit/component tests will utilize xUnit framework.
- tools:
any tools and components that may be used during the development (such as event hub event producer)
or CI/CD required shell commands (such as powershell files)

## Prerequisites

## Local Development

In order to work with this repo in your local development environment, you will need to install the following packages:

- [.Net 6.0](https://dotnet.microsoft.com/en-us/download)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Azure Functions Core Tools](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=enterprise&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio)
  - After installation, it needs to be manually started using Azurite commands
- All required extensions will be recommended to be installed when opening the workspace (check extensions.json file in .vscode folder).

## More Links

- The project uses [central package management feature](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management).
So all C# projects that need to to install a nuget package,
they should specify the version of the nuget package in Directory.Packages.props file in solution level.
