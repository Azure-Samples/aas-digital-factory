resource "azurerm_kusto_cluster" "cluster" {
  name                = var.adx_cluster_name
  location            = var.location
  resource_group_name = var.resource_group_name

  sku {
    name     = var.adx_cluster_sku_name
    capacity = var.adx_cluster_capacity
  }

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_kusto_database" "database" {
  name                = var.adx_database_name
  location            = var.location
  resource_group_name = var.resource_group_name
  cluster_name        = azurerm_kusto_cluster.cluster.name
  hot_cache_period    = var.adx_database_hot_cache_period
  soft_delete_period  = var.adx_database_soft_delete_period
}

data "azurerm_client_config" "current" {}

resource "azurerm_kusto_database_principal_assignment" "adt_database_principal_assignment" {
  name                = "KustoPrincipalAssignment"
  resource_group_name = var.resource_group_name
  cluster_name        = azurerm_kusto_cluster.cluster.name
  database_name       = azurerm_kusto_database.database.name

  tenant_id      = data.azurerm_client_config.current.tenant_id
  principal_id   = azurerm_digital_twins_instance.adt.identity[0].principal_id
  principal_type = "App"
  role           = "Admin"
}

resource "azurerm_kusto_script" "historization_table" {
  name                       = "historization_table_script"
  database_id                = azurerm_kusto_database.database.id
  continue_on_errors_enabled = false
  script_content             = ".create table ${var.adx_table_name} (TimeStamp: datetime, SourceTimeStamp: datetime, ServiceId: string, Id: string, ModelId: string, Key: string, Value: dynamic, RelationshipTarget: string, RelationshipId: string)"
}

resource "azurerm_kusto_script" "historization_mapping_rule" {
  name                       = "historization_mapping_rule_script"
  database_id                = azurerm_kusto_database.database.id
  continue_on_errors_enabled = false
  script_content             = ".create table ${var.adx_table_name} ingestion json mapping '${var.adx_mapping_rule_name}' '[{\"column\":\"TimeStamp\",\"path\":\"$.timeStamp\",\"datatype\":\"\",\"transform\":null},{\"column\":\"SourceTimeStamp\",\"path\":\"$.sourceTimeStamp\",\"datatype\":\"\",\"transform\":null},{\"column\":\"ServiceId\",\"path\":\"$.serviceId\",\"datatype\":\"\",\"transform\":null},{\"column\":\"Id\",\"path\":\"$.id\",\"datatype\":\"\",\"transform\":null},{\"column\":\"ModelId\",\"path\":\"$.modelId\",\"datatype\":\"\",\"transform\":null},{\"column\":\"Key\",\"path\":\"$.key\",\"datatype\":\"\",\"transform\":null},{\"column\":\"Value\",\"path\":\"$.value\",\"datatype\":\"\",\"transform\":null},{\"column\":\"RelationshipTarget\",\"path\":\"$.relationshipTarget\",\"datatype\":\"\",\"transform\":null},{\"column\":\"RelationshipId\",\"path\":\"$.relationshipId\",\"datatype\":\"\",\"transform\":null}]'"
  depends_on                 = [azurerm_kusto_script.historization_table]
}