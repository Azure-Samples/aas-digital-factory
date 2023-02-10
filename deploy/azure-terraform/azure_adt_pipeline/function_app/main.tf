resource "azurerm_storage_account" "function_storage_account" {
  name                     = var.storage_account_name
  resource_group_name      = var.resource_group_name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = var.storage_account_replication_type

  allow_nested_items_to_be_public = false

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_service_plan" "function_service_plan" {
  name                = var.service_plan_name
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = "Linux"

  sku_name = var.service_plan_sku_name

  maximum_elastic_worker_count = var.maximum_elastic_worker_count

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_linux_function_app" "function_app" {
  name                       = var.function_app_name
  location                   = var.location
  resource_group_name        = var.resource_group_name
  service_plan_id            = azurerm_service_plan.function_service_plan.id
  storage_account_name       = azurerm_storage_account.function_storage_account.name
  storage_account_access_key = azurerm_storage_account.function_storage_account.primary_access_key

  functions_extension_version = "~4"
  https_only                  = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    application_insights_connection_string = var.app_insights_connection_string
    application_insights_key               = var.app_insights_instrumentation_key
    elastic_instance_minimum               = var.elastic_instance_minimum
  }

  app_settings = var.function_app_app_settings

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}
