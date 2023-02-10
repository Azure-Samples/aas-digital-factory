# Infrastructure Setup <!-- omit in toc -->

This design document illustrates the infrastructure requirements of Model Data & Telemetry Update Flows to ensure the deployments in this project.

## Sections <!-- omit in toc -->

- [Overview](#overview)
- [Requirements](#requirements)
- [Out of scope of this sample project](#out-of-scope-of-this-sample-project)
- [Design](#design)
  - [Terraform Module Structure](#terraform-module-structure)
  - [Terraform Resources](#terraform-resources)
    - [Event Hubs](#event-hubs)
    - [Storage Accounts](#storage-accounts)
    - [Digital Twin Instances](#digital-twin-instances)
    - [Function Apps and App Service Plans](#function-apps-and-app-service-plans)
    - [App Insights](#app-insights)
  - [Resources Naming Convention](#resources-naming-convention)
  - [Deployment Strategy for IaC](#deployment-strategy-for-iac)

## Overview

The fundamental concept of Infrastructure as Code (IaC) is relatively simple: manage automated deployment with the best practices of
DevOps. As in its name, code is key. Thus, the code used to manage infrastructure deployment will follow the same pattern as any other
type of source code in the project. The code should require proper development with best practices, it should be modularized to reduce
code duplication, it should be tested automatically, and deployed with proper CI/CD pipelines.

## Requirements

- Use Terraform as main IaC tool for provisioning and managing cloud infrastructure.
- IaC files are defined as part of [this repository](../../deploy/../README.md).
- IaC files should be modularized.
- Terraform state hosted in Azure Storage. More info can be found [here](https://learn.microsoft.com/en-us/azure/developer/terraform/store-state-in-azure-storage?tabs=azure-cli)
- All Azure resources must be tagged:
  - `env` as the name of the environment.
  - `description` as "Managed by Terraform".
- Terraform lock file should be in source control.
  To learn more about the lock file, click [here](https://developer.hashicorp.com/terraform/language/files/dependency-lock).

## Out of scope of this sample project

- Use of an automated CI/CD platform as the main tool for:
  - Validating IaC files
  - Storing the Terraform execution plan
  - Keeping up-to-date Azure resources, AAS-DigitalTwins metadata models and Azure Functions.
code based from `master` branch.
- Private networking (VNets, private endpoints, NSGs, etc.)

## Design

### Terraform Module Structure

Data Model and Telemetry Model flows will use similar Azure resources. Based on this pattern, we can create modules for
common components, so that each Azure Function module will re-use them.

Thus the module structure would look like the following:

``` text
├── modules
│   ├── storage-account.tf
│   ├── digital-twins.tf
│   ├── event-hubs.tf
│   └── app-insights.tf
├── functions.tf
├── main.tf
├── variables.tf
│
```

The parameters for the entry `main.tf` file are the following:

- env: The name of the environment
- prefix: The naming prefix
- location: Name of the location (eastus, westus, etc.)

### Terraform Resources

#### Event Hubs

This project will contain one Event Hub Namespace with multiple Event Hubs.
Each Event Hub will trigger an Azure Function to perform some operation.
Since each Azure Function connect to the Event Hub,
each Azure Function will need the `Azure Event Hubs Data Sender` and `Azure Event Hubs Data Receiver` roles.
This role is needed as each of the Function App will need to both send and receive Event Hub events.

The Event Hub Namespace price plan will be `Standard`, capacity of 1 consumer and event retention of 7 days.
See more details [here](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-quotas).

#### Storage Accounts

This project will contain three Storage accounts.
There will be a storage account for each of the Azure Function App and one more for the data processing (model data flow).
The Azure function will not need any special roles for the storage account that will be used for the `AzureWebJobsStorage` connection,
but the model data Azure Function will need the `Storage Blob Data Contributor` role to read and write blobs.

The Storage Account price plan will be `Standard`. Additionally, blob versioning will be enabled for recovery and restoration.

#### Digital Twin Instances

This project will contain a single Digital Twins instance.
Both Azure functions will access to this Digital Twins instance to create, replace or delete twins and relationships.
We will use a public network. We may explore options for configure a private network and not allow external
customers to access this instance.

#### Function Apps and App Service Plans

This project will contain two Function Apps and two App Service Plans.

The model data flow function app will contain the following key-value pairs in their Application Settings:

- **FACTORY_EVENT_HUB_NAME**: The Event Hub name for Factory Raw data to AAS
- **AAS_EVENT_HUB_NAME**: The Event Hub name for AAS to ADT
- **EVENT_HUB_CONNECTION_STRING**: The Event Hub namespace connection string
- **STORAGE_ACCOUNT_CONTAINER_NAME**: The Storage Account container to access
- **STORAGE_ACCOUNT_CONNECTION_STRING**: The Storage Account connection string
- **ADT_INSTANCE_URL**: Hostname of the Digital Twins instance
- **ABBREVIATED_COMPANY_NAME**: The abbreviated name of company
- **SHELLS_STORAGE_PATH**: The Storage Account path which AAS Shells are stored.

The telemetry data flow function app will contain the following key-pairs in their Application Settings:

- **FACTORY_EVENT_HUB_NAME**: The Event Hub name for Factory Raw data to AAS
- **AAS_EVENT_HUB_NAME**: The Event Hub name for AAS to ADT
- **EVENT_HUB_CONNECTION_STRING**: The Event Hub namespace connection string
- **STORAGE_ACCOUNT_CONTAINER_NAME**: The Storage Account container to access
- **STORAGE_ACCOUNT_CONNECTION_STRING**: The Storage Account connection string
- **ADT_INSTANCE_URL**: Hostname of the Digital Twins instance

The Azure Functions should be able to authenticate and access to the Azure Digital Twins instance designated and blob
storage accounts. It will use a managed identity with `Azure Digital Twins Data Owner` and `Storage Blob Data Contributor` roles.
The Azure Functions will also need the `Azure Event Hubs Data Sender` and `Azure Event Hubs Data Receiver` roles
to use the fully qualified namespace when connecting to Event Hub.

The app service plans for both Azure Function Apps will run on a `Standard` tier as dedicated compute and size `S1`. See more details [here](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans).

#### App Insights

This project will contain a single workspace-based App Insights instance.
Since the App Insights instance will be workspace-based,
a Log Analytics Workspace will need to be created and connected to the App Insights instance.

### Resources Naming Convention

The expected naming convention of the Azure resources is:
> `<prefix>-<environment>-<location>-<type>-<resourcename>`

Here is a good example for an EventHub namespace in dev environment is:
> aas-dev-westus2-ehns-001

For additional guidance, please visit
[Recommended naming and tagging conventions](https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).

Here are the details:

| Naming Component | Description | Examples |
| ---------------- | ----------- | -------- |
| `<prefix>` | Naming prefix | `aas` - AAS Factory |
| `<environment>` | Environment of the deployment | `dev` - development, `test` - Test/Stage, `prod` - Production |
| `<location>` | Region the resource is created | `nc` - North Central, `westus2` - US West 2 |
| `<type>` | Type of resource | `dt` - digital twin instance, `ehns` - eventhub namespace, `eh` - eventhub, `st` - storage account|
| `<resourcename>` | Name or acronym of the resource to create | `rawtoaas`, `001` |

As an exception, for those Azure services (i.e. Storage Account) which don't support hyphens (`-`) in their resource name, hyphens will be take out.

### Deployment Strategy for IaC

Automated deployments are not implemented in this sample project; the instructions below are provided as reference for future extension.

The IaC can be automatically deployed using the terraform CLI.

Before running any deployment, there are pre-requisites that must be met:

- The user or deployment tool must have credentials to authenticate to Azure: Client Id, Client Secret, Subscription Id and Tenant Id.
- The user or deployment tool must have the enough permissions to create resources and assign roles.
- Terraform state hosted in Azure Storage is already setup.
- Terraform configuration must be initialized.

```bash
terraform init
```

Best practice is that every time new changes are merged to `master` branch, the CI pipeline will create and persist a new execution plan. This can be done manually by running the same command to preview the changes that Terraform plans to execute:

```bash
terraform plan -out tfplan
```

To make the infrastructure changes based on the TF plan, run:

```bash
terraform apply tfplan
```
