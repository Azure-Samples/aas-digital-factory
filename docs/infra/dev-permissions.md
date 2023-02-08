# IAM Permissions <!-- omit in toc -->

This document is intended to outline the necessary permissions that developers need and how to create those roles

## Table of Contents <!-- omit in toc -->

- [Necessary Roles](#necessary-roles)
  - [Azure Data Explorer (ADX) Database](#azure-data-explorer-adx-database)
    - [Roles](#roles)
  - [Steps](#steps)
  - [Azure Digital Twins](#azure-digital-twins)
    - [Developer Permissions](#developer-permissions)

## Necessary Roles

Before adding roles to a resource, you must know how to add roles in Azure.
[This document](https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-portal) outlines the process on how to assign roles
using the Azure Portal.
Now, that that's out of the way, The following are the roles that will be needed for developers for this project.

### Azure Data Explorer (ADX) Database

While a specific role is not needed on the ADX resource itself (as long as you have access to it),
you will need to have **at least** the `Viewer` role for the `historizationdb`.
The process of setting up this role is a bit different than setting up the roles in IAM.

#### Roles

For this application, the `Viewer` permissions should be sufficient for most developers.

### Steps

1. Navigate to the ADX instance and click on the `Databases` tab under `Data`. Click on the corresponding database you want to add permissions to.

2. Click on the `Permissions` tab under `Overview`. Click on Add on the top of the screen and select the permission you would like to add.
   There will be a pop-up on the side which will prompt you to search for the principal to assign the permission to.

### Azure Digital Twins

Azure Digital twins is a bit more straight forward.
Follow [this document](https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-portal) on assigning roles if you aren't
quite familiar with assigning roles.

#### Developer Permissions

For this application, developers will need the `Azure Digital Twins Data Owner` role.
