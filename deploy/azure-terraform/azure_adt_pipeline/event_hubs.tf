resource "azurerm_eventhub_namespace" "eventhub_namespace" {
  name                = var.eventhub_namespace_name
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = var.eventhub_namespace_sku
  zone_redundant      = var.eventhub_namespace_sku == "Premium" ? true : var.eventhub_zone_redundant
  capacity            = var.eventhub_namespace_capacity

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_eventhub" "factory_model_data_changed_eventhub" {
  name                = var.factory_model_data_changed_eventhub_name
  namespace_name      = azurerm_eventhub_namespace.eventhub_namespace.name
  resource_group_name = var.resource_group_name
  partition_count     = var.model_data_eventhub_partition_count
  message_retention   = var.model_data_eventhub_message_retention
}

resource "azurerm_eventhub" "aas_model_data_changed_eventhub" {
  name                = var.aas_model_data_changed_eventhub_name
  namespace_name      = azurerm_eventhub_namespace.eventhub_namespace.name
  resource_group_name = var.resource_group_name
  partition_count     = var.model_data_eventhub_partition_count
  message_retention   = var.model_data_eventhub_message_retention
}

resource "azurerm_eventhub" "factory_telemetry_data_changed_eventhub" {
  name                = var.factory_telemetry_data_changed_eventhub_name
  namespace_name      = azurerm_eventhub_namespace.eventhub_namespace.name
  resource_group_name = var.resource_group_name
  partition_count     = var.telemetry_data_eventhub_partition_count
  message_retention   = var.telemetry_data_eventhub_message_retention
}

resource "azurerm_eventhub" "aas_telemetry_data_changed_eventhub" {
  name                = var.aas_telemetry_data_changed_eventhub_name
  namespace_name      = azurerm_eventhub_namespace.eventhub_namespace.name
  resource_group_name = var.resource_group_name
  partition_count     = var.telemetry_data_eventhub_partition_count
  message_retention   = var.telemetry_data_eventhub_message_retention
}

resource "azurerm_eventhub" "adx_eventhub" {
  name                = var.adt_data_changed_eventhub_name
  namespace_name      = azurerm_eventhub_namespace.eventhub_namespace.name
  resource_group_name = var.resource_group_name
  partition_count     = var.data_history_eventhub_partition_count
  message_retention   = var.data_history_eventhub_message_retention
}