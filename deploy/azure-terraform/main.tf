locals {
  resource_name_prefix_dashes    = "${var.prefix}-${var.env}-${var.location}"
  resource_name_prefix_no_dashes = "${var.prefix}${var.env}${var.location}"

  model_data_function_app_name     = "${local.resource_name_prefix_dashes}-md-fn"
  telemetry_data_function_app_name = "${local.resource_name_prefix_dashes}-td-fn"

  model_data_storage_account_name = "${local.resource_name_prefix_no_dashes}sa"
}

resource "azurerm_resource_group" "rg" {
  name     = local.resource_name_prefix_dashes
  location = var.location
}

module "azure_adt_pipeline" {
  # General variables
  source              = "./azure_adt_pipeline"
  env                 = var.env
  location            = var.location
  resource_group_name = azurerm_resource_group.rg.name

  # App Insights variables
  app_insights_name                         = "${local.resource_name_prefix_dashes}-ai"
  log_analytics_workspace_name              = "${local.resource_name_prefix_dashes}-la"
  log_analytics_workspace_retention_in_days = var.log_analytics_workspace_retention_in_days

  # ADT variables
  adt_name            = "${local.resource_name_prefix_dashes}-adt"
  adt_models_link      = var.adt_models_link
  adt_models_directory = var.adt_models_directory

  # ADX variables
  adx_cluster_name                = "${local.resource_name_prefix_no_dashes}adx"
  adx_database_name               = var.adx_database_name
  adx_cluster_sku_name            = var.adx_cluster_sku_name
  adx_cluster_capacity            = var.adx_cluster_capacity
  adx_database_hot_cache_period   = var.adx_database_hot_cache_period
  adx_database_soft_delete_period = var.adx_database_soft_delete_period
  adx_table_name                  = var.adx_table_name
  adx_mapping_rule_name           = var.adx_mapping_rule_name

  # EventHubs variables
  eventhub_namespace_capacity = var.eventhub_namespace_capacity
  eventhub_namespace_name     = "${local.resource_name_prefix_dashes}-ehns"
  eventhub_namespace_sku      = var.eventhub_namespace_sku
  eventhub_zone_redundant     = var.eventhub_zone_redundant

  model_data_eventhub_partition_count       = var.model_data_eventhub_partition_count
  model_data_eventhub_message_retention     = var.model_data_eventhub_message_retention
  telemetry_data_eventhub_partition_count   = var.telemetry_data_eventhub_partition_count
  telemetry_data_eventhub_message_retention = var.telemetry_data_eventhub_message_retention
  data_history_eventhub_message_retention   = var.data_history_eventhub_message_retention
  data_history_eventhub_partition_count     = var.data_history_eventhub_partition_count

  aas_model_data_changed_eventhub_name         = var.aas_model_data_changed_eventhub_name
  aas_telemetry_data_changed_eventhub_name     = var.aas_telemetry_data_changed_eventhub_name
  factory_model_data_changed_eventhub_name     = var.factory_model_data_changed_eventhub_name
  factory_telemetry_data_changed_eventhub_name = var.factory_telemetry_data_changed_eventhub_name
  adt_data_changed_eventhub_name               = var.adt_data_changed_eventhub_name

  # Function app variables
  function_app_storage_account_replication_type = var.function_app_storage_account_replication_type
  model_data_function_app_name                  = local.model_data_function_app_name
  model_data_service_plan_sku_name              = var.model_data_service_plan_sku_name
  model_data_maximum_elastic_worker_count       = var.model_data_maximum_elastic_worker_count
  model_data_function_app_service_plan_name     = "${local.resource_name_prefix_dashes}-md-sp"
  model_data_function_app_storage_account_name  = "${local.resource_name_prefix_no_dashes}mdsa"

  telemetry_data_function_app_name                 = local.telemetry_data_function_app_name
  telemetry_data_service_plan_sku_name             = var.telemetry_data_service_plan_sku_name
  telemetry_data_maximum_elastic_worker_count      = var.telemetry_data_maximum_elastic_worker_count
  telemetry_data_elastic_instance_minimum          = var.telemetry_data_elastic_instance_minimum
  telemetry_data_function_app_service_plan_name    = "${local.resource_name_prefix_dashes}-td-sp"
  telemetry_data_function_app_storage_account_name = "${local.resource_name_prefix_no_dashes}tdsa"

  abbreviated_company_name                = var.abbreviated_company_name
  shell_storage_path                      = var.shell_storage_path
  circuit_breaker_allowed_exception_count = var.circuit_breaker_allowed_exception_count
  circuit_breaker_wait_time_sec           = var.circuit_breaker_wait_time_sec

  # Storage account variables
  model_data_storage_account_name             = local.model_data_storage_account_name
  model_data_storage_container_name           = var.model_data_storage_container_name
  model_data_storage_account_replication_type = var.model_data_storage_account_replication_type
  model_data_storage_account_retention_days   = var.model_data_storage_account_retention_days
}
