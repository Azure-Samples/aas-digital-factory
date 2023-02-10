module "model_data_function_app" {
  source                           = "./function_app"
  env                              = var.env
  location                         = var.location
  resource_group_name              = var.resource_group_name
  storage_account_name             = var.model_data_function_app_storage_account_name
  storage_account_replication_type = var.function_app_storage_account_replication_type
  service_plan_name                = var.model_data_function_app_service_plan_name
  function_app_name                = var.model_data_function_app_name

  service_plan_sku_name = var.model_data_service_plan_sku_name

  maximum_elastic_worker_count = var.model_data_maximum_elastic_worker_count
  elastic_instance_minimum     = 1

  function_app_app_settings = {
    FUNCTIONS_WORKER_RUNTIME                             = "dotnet"
    FACTORY_EVENT_HUB_NAME                               = azurerm_eventhub.factory_model_data_changed_eventhub.name
    AAS_EVENT_HUB_NAME                                   = azurerm_eventhub.aas_model_data_changed_eventhub.name
    EVENT_HUB_CONNECTION_STRING__fullyQualifiedNamespace = "${var.eventhub_namespace_name}.servicebus.windows.net"
    STORAGE_ACCOUNT_CONTAINER_NAME                       = var.model_data_storage_container_name
    STORAGE_ACCOUNT_CONNECTION_STRING                    = "https://${var.model_data_storage_account_name}.blob.core.windows.net"
    ADT_INSTANCE_URL                                     = "https://${azurerm_digital_twins_instance.adt.host_name}"
    SHELLS_STORAGE_PATH                                  = var.shell_storage_path
    ABBREVIATED_COMPANY_NAME                             = var.abbreviated_company_name
  }

  app_insights_connection_string   = azurerm_application_insights.app_insights.connection_string
  app_insights_instrumentation_key = azurerm_application_insights.app_insights.instrumentation_key
}

module "telemetry_data_function_app" {
  source                           = "./function_app"
  env                              = var.env
  location                         = var.location
  resource_group_name              = var.resource_group_name
  storage_account_name             = var.telemetry_data_function_app_storage_account_name
  storage_account_replication_type = var.function_app_storage_account_replication_type
  service_plan_name                = var.telemetry_data_function_app_service_plan_name
  function_app_name                = var.telemetry_data_function_app_name

  service_plan_sku_name = var.telemetry_data_service_plan_sku_name

  maximum_elastic_worker_count = var.telemetry_data_maximum_elastic_worker_count
  elastic_instance_minimum     = var.telemetry_data_elastic_instance_minimum

  function_app_app_settings = {
    FUNCTIONS_WORKER_RUNTIME                             = "dotnet"
    FACTORY_EVENT_HUB_NAME                               = azurerm_eventhub.factory_telemetry_data_changed_eventhub.name
    AAS_EVENT_HUB_NAME                                   = azurerm_eventhub.aas_telemetry_data_changed_eventhub.name
    EVENT_HUB_CONNECTION_STRING__fullyQualifiedNamespace = "${var.eventhub_namespace_name}.servicebus.windows.net"
    ADT_INSTANCE_URL                                     = "https://${azurerm_digital_twins_instance.adt.host_name}"
    CIRCUIT_BREAKER_ALLOWED_EXCEPTION_COUNT              = var.circuit_breaker_allowed_exception_count
    CIRCUIT_BREAKER_WAIT_TIME_SEC                        = var.circuit_breaker_wait_time_sec
  }

  app_insights_connection_string   = azurerm_application_insights.app_insights.connection_string
  app_insights_instrumentation_key = azurerm_application_insights.app_insights.instrumentation_key
}

# Role Assignments
resource "azurerm_role_assignment" "model_update_function_eventhub_sender_role_assignment" {
  role_definition_name = "Azure Event Hubs Data Sender"
  scope                = azurerm_eventhub_namespace.eventhub_namespace.id
  principal_id         = module.model_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "telemetry_update_function_eventhub_sender_hub_role_assignment" {
  role_definition_name = "Azure Event Hubs Data Sender"
  scope                = azurerm_eventhub_namespace.eventhub_namespace.id
  principal_id         = module.telemetry_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "model_update_function_eventhub_receiver_role_assignment" {
  role_definition_name = "Azure Event Hubs Data Receiver"
  scope                = azurerm_eventhub_namespace.eventhub_namespace.id
  principal_id         = module.model_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "telemetry_update_function_eventhub_receiver_hub_role_assignment" {
  role_definition_name = "Azure Event Hubs Data Receiver"
  scope                = azurerm_eventhub_namespace.eventhub_namespace.id
  principal_id         = module.telemetry_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "model_update_function_azure_digital_twins_role_assignment" {
  role_definition_name = "Azure Digital Twins Data Owner"
  scope                = azurerm_digital_twins_instance.adt.id
  principal_id         = module.model_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "telemetry_update_function_azure_digital_twins_role_assignment" {
  role_definition_name = "Azure Digital Twins Data Owner"
  scope                = azurerm_digital_twins_instance.adt.id
  principal_id         = module.telemetry_data_function_app.function_app_principal_id
}

resource "azurerm_role_assignment" "model_update_function_storage_account_role_assignment" {
  role_definition_name = "Storage Blob Data Contributor"
  scope                = azurerm_storage_account.model_data_storage_account.id
  principal_id         = module.model_data_function_app.function_app_principal_id
}