resource "azurerm_digital_twins_instance" "adt" {
  name                = var.adt_name
  resource_group_name = var.resource_group_name
  location            = var.location

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azapi_resource" "adt_to_adx_data_history_connection" {
  type      = "Microsoft.DigitalTwins/digitalTwinsInstances/timeSeriesDatabaseConnections@2022-05-31"
  name      = "AdtAdxConnection"
  parent_id = azurerm_digital_twins_instance.adt.id
  body = jsonencode({
    properties = {
      connectionType  = "AzureDataExplorer"
      adxDatabaseName = azurerm_kusto_database.database.name
      adxEndpointUri  = azurerm_kusto_cluster.cluster.uri
      adxResourceId   = azurerm_kusto_cluster.cluster.id
      adxTableName    = var.adx_table_name

      eventHubEndpointUri         = "sb://${var.eventhub_namespace_name}.servicebus.windows.net"
      eventHubEntityPath          = azurerm_eventhub.adx_eventhub.name
      eventHubNamespaceResourceId = azurerm_eventhub_namespace.eventhub_namespace.id
    }
  })

  depends_on = [
    azurerm_role_assignment.adt_eventhub_role_assignment,
    azurerm_role_assignment.adt_adx_role_assignment,
    azurerm_kusto_database_principal_assignment.adt_database_principal_assignment,
    azurerm_kusto_script.historization_table,
    azurerm_kusto_script.historization_mapping_rule
  ]
}

# Role Assignments
resource "azurerm_role_assignment" "adt_eventhub_role_assignment" {
  role_definition_name = "Azure Event Hubs Data Owner"
  scope                = azurerm_eventhub_namespace.eventhub_namespace.id
  principal_id         = azurerm_digital_twins_instance.adt.identity[0].principal_id
}

resource "azurerm_role_assignment" "adt_adx_role_assignment" {
  role_definition_name = "Contributor"
  scope                = azurerm_kusto_database.database.id
  principal_id         = azurerm_digital_twins_instance.adt.identity[0].principal_id
}


data "azurerm_client_config" "current_one" {}

data "azuread_user" "current_object_id" {
  object_id = data.azurerm_client_config.current_one.object_id
}

resource "azurerm_role_assignment" "service_principal_adt_role_assignment" {
  role_definition_name = "Azure Digital Twins Data Owner"
  scope                = azurerm_digital_twins_instance.adt.id
  principal_id         = data.azuread_user.current_object_id.object_id
}


resource "null_resource" "update_adt_metamodels" {
  provisioner "local-exec" {
    command = <<EOT
      sh azure_adt_pipeline/scripts/update_adt_models.sh  -n ${var.adt_name} -m ${var.adt_models_link} -d ${var.adt_models_directory}
    EOT
  }

  triggers = {
    adt_id = azurerm_digital_twins_instance.adt.id
  }

  depends_on = [
    azurerm_role_assignment.service_principal_adt_role_assignment
  ]
}