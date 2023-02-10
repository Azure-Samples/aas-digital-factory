# General variables
variable "env" {
  type        = string
  description = "The environment."
}

variable "location" {
  type        = string
  description = "The name of the location (eastus,westus,etc.)."
}

variable "resource_group_name" {
  type        = string
  description = "The name of the resource group."
}

# Application Insights variables
variable "app_insights_name" {
  type        = string
  description = "The name of the app insights instance."
}

variable "log_analytics_workspace_name" {
  type        = string
  description = "The name of the log analytics workspace."
}

variable "log_analytics_workspace_retention_in_days" {
  type        = number
  description = "The retention policy for the log analytics workspace."
}

variable "alert_action_group_email_address" {
  type        = string
  description = "The email address to receive notifications when Azure Monitor alerts are triggered."
}

variable "mdf_alert_severity" {
  type        = number
  description = "The severity of the Model Data Flow exception alert."
}

variable "mdf_alert_exception_threshold" {
  type        = number
  description = "The number of exceptions that the Model Data function can encounter in a 5 minute period before an alert is triggered."
}

variable "tdf_alert_severity" {
  type        = number
  description = "The severity of the Streaming Data Flow exception alert."
}

variable "tdf_alert_exception_threshold" {
  type        = number
  description = "The number of exceptions that the Streaming Data function can encounter in a 5 minute period before an alert is triggered."
}

# Azure Data Explorer variables
variable "adx_cluster_name" {
  type        = string
  description = "The name of the Azure Data Explorer cluster."
}

variable "adx_database_name" {
  type        = string
  description = "The name of the database within Azure Data Explorer."
}

variable "adx_table_name" {
  type        = string
  description = "The name of the ADX table"
}

variable "adx_mapping_rule_name" {
  type        = string
  description = "The name of the mapping rule for ADX"
}

variable "adx_cluster_sku_name" {
  type        = string
  description = "The sku name of the Azure Data Explorer cluster."
}

variable "adx_cluster_capacity" {
  type        = number
  description = "The capacity of the Azure Data Explorer cluster."
}

variable "adx_database_hot_cache_period" {
  type        = string
  description = "The hot cache period of the Azure Data Explorer database."
}

variable "adx_database_soft_delete_period" {
  type        = string
  description = "The soft delete period of the Azure Data Explorer database."
}

# Azure Digital Twins variables
variable "adt_name" {
  type        = string
  description = "The name of the Azure Digital Twins instance."
}

# EventHubs variables
variable "aas_model_data_changed_eventhub_name" {
  type        = string
  description = "The name of the AAS model data changed EventHub."
}

variable "aas_telemetry_data_changed_eventhub_name" {
  type        = string
  description = "The name of the AAS telemetry data changed EventHub."
}

variable "eventhub_namespace_capacity" {
  type        = number
  description = "The name of the EventHub namespace capacity/throughput units."
}

variable "eventhub_namespace_name" {
  type        = string
  description = "The name of the EventHub namespace."
}

variable "eventhub_namespace_sku" {
  type        = string
  description = "The name of the EventHub namespace sku/tier."
}

variable "eventhub_zone_redundant" {
  type        = bool
  description = "Zone redundant should be enabled or not"
}

variable "factory_model_data_changed_eventhub_name" {
  type        = string
  description = "The name of the factory model data changed EventHub."
}

variable "factory_telemetry_data_changed_eventhub_name" {
  type        = string
  description = "The name of the factory telemetry data changed EventHub."
}

variable "model_data_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the model data EventHub event."
}

variable "model_data_eventhub_partition_count" {
  type        = number
  description = "The partition count for the model data EventHub."
}

variable "adt_data_changed_eventhub_name" {
  type        = string
  description = "The name of the Digital Twins EventHub."
}

variable "telemetry_data_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the telemetry data EventHub event."
}

variable "telemetry_data_eventhub_partition_count" {
  type        = number
  description = "The partition count for the telemetry data EventHub."
}

# Function App variables
variable "function_app_storage_account_replication_type" {
  type        = string
  description = "The replication type of the function app's storage account."
}

variable "model_data_function_app_name" {
  type        = string
  description = "The name of the model data function app."
}

variable "model_data_function_app_service_plan_name" {
  type        = string
  description = "The name of the model data function app's service plan."
}

variable "model_data_function_app_storage_account_name" {
  type        = string
  description = "The name of the model data function app's storage account."
}

variable "model_data_service_plan_sku_name" {
  type        = string
  description = "The tier and size of the model data service plan."
}

variable "model_data_maximum_elastic_worker_count" {
  type        = number
  description = "The maximum number of elastic model data workers."
}

variable "telemetry_data_function_app_name" {
  type        = string
  description = "The name of the telemetry data function app."
}

variable "telemetry_data_function_app_service_plan_name" {
  type        = string
  description = "The name of the telemetry data function app's service plan."
}

variable "telemetry_data_function_app_storage_account_name" {
  type        = string
  description = "The name of the telemetry data function app's storage account."
}

variable "telemetry_data_service_plan_sku_name" {
  type        = string
  description = "The tier and size of the telemetry data service plan."
}

variable "telemetry_data_elastic_instance_minimum" {
  type        = number
  description = "The minimum number of elastic telemetry data workers. This is only for Premium app"
}

variable "telemetry_data_maximum_elastic_worker_count" {
  type        = number
  description = "The maximum number of elastic telemetry data workers."
}

# Function App App Setting variables
variable "abbreviated_company_name" {
  type        = string
  description = "The abbreviated company name."
}

variable "shell_storage_path" {
  type        = string
  description = "The path to store the shells in the storage account."
}

variable "circuit_breaker_allowed_exception_count" {
  type        = number
  description = "The number of consecutive exceptions allowed before the circuit breaks for ADT SDK calls."
}

variable "circuit_breaker_wait_time_sec" {
  type        = number
  description = "The duration of the circuit break (in seconds) after the number of max allowed exceptions is reached."
}

# Storage account variables
variable "model_data_storage_account_name" {
  type        = string
  description = "The name of the model data storage account."
}

variable "model_data_storage_container_name" {
  type        = string
  description = "The name of the model data storage container."
}

variable "model_data_storage_account_replication_type" {
  type        = string
  description = "The replication type of the model data storage account."
}

variable "model_data_storage_account_retention_days" {
  type        = number
  description = "Number of days to keep the backups"
}

variable "data_history_eventhub_partition_count" {
  type        = number
  description = "The partition count for the Azure Data Explorer EventHub."
}

variable "data_history_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the Azure Data Explorer EventHub."
}

variable "adt_models_link" {
  type        = string
  description = "Link to download ADT metamodel JSON files."
}

variable "adt_models_directory" {
  type        = string
  description = "Directory inside adt models zip that contains the model JSON files."
}