# General variables
variable "env" {
  type        = string
  description = "The environment."
  default     = "dev"
}

variable "location" {
  type        = string
  description = "The name of the location (eastus,westus,etc.)."
  default     = "eastus2"
}

variable "prefix" {
  type        = string
  description = "The prefix for the resources."
}

# Azure Data Explorer variables
variable "adx_database_name" {
  type        = string
  description = "The name of the database within Azure Data Explorer."
  default     = "historizationdb"
}

variable "adx_table_name" {
  type        = string
  description = "The name of the ADX table"
  default     = "historization_table"
}

variable "adx_mapping_rule_name" {
  type        = string
  description = "The name of the mapping rule for ADX"
  default     = "historization_mapping_rule"
}

variable "adx_cluster_sku_name" {
  type        = string
  description = "The sku name of the Azure Data Explorer cluster."
  default     = "Standard_D13_v2"
}

variable "adx_cluster_capacity" {
  type        = number
  description = "The capacity of the Azure Data Explorer cluster."
  default     = 2
}

variable "adx_database_hot_cache_period" {
  type        = string
  description = "The hot cache period of the Azure Data Explorer database."
  default     = "P7D"
}

variable "adx_database_soft_delete_period" {
  type        = string
  description = "The soft delete period of the Azure Data Explorer database."
  default     = "P31D"
}

# Application Insights variables
variable "log_analytics_workspace_retention_in_days" {
  type        = number
  description = "The retention policy for the log analytics workspace."
  default     = 30
}

variable "alert_action_group_email_address" {
  type        = string
  description = "The email address to receive notifications when Azure Monitor alerts are triggered."
}

variable "mdf_alert_severity" {
  type        = number
  description = "The severity of the Model Data Flow exception alert."
  default     = 1
}

variable "mdf_alert_exception_threshold" {
  type        = number
  description = "The number of exceptions that the Model Data function can encounter in a 5 minute period before an alert is triggered."
  default     = 1
}

variable "tdf_alert_severity" {
  type        = number
  description = "The severity of the Streaming Data Flow exception alert."
  default     = 2
}

variable "tdf_alert_exception_threshold" {
  type        = number
  description = "The number of exceptions that the Streaming Data function can encounter in a 5 minute period before an alert is triggered."
  default     = 20
}

# EventHubs variables
variable "eventhub_namespace_sku" {
  type        = string
  description = "The name of the EventHub namespace sku/tier."
  default     = "Standard"
}

variable "eventhub_zone_redundant" {
  type        = bool
  description = "Zone redundant should be enabled or not"
  default     = false
}

variable "eventhub_namespace_capacity" {
  type        = number
  description = "The name of the EventHub namespace capacity/throughput units."
  default     = 1
}

variable "model_data_eventhub_partition_count" {
  type        = number
  description = "The partition count for the model data EventHub."
  default     = 1
}

variable "model_data_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the model data EventHub event."
  default     = 1
}

variable "telemetry_data_eventhub_partition_count" {
  type        = number
  description = "The partition count for the telemetry data EventHub."
  default     = 30
}

variable "telemetry_data_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the telemetry data EventHub event."
  default     = 1
}

variable "data_history_eventhub_partition_count" {
  type        = number
  description = "The partition count for the Azure Data Explorer EventHub."
  default     = 2
}

variable "data_history_eventhub_message_retention" {
  type        = number
  description = "The message retention (in days) of the Azure Data Explorer EventHub."
  default     = 1
}

variable "factory_model_data_changed_eventhub_name" {
  type        = string
  description = "The name of the factory model data changed EventHub."
  default     = "factory-model-data-changed-eh"
}

variable "factory_telemetry_data_changed_eventhub_name" {
  type        = string
  description = "The name of the factory telemetry data changed EventHub."
  default     = "factory-telemetry-data-changed-eh"
}

variable "aas_model_data_changed_eventhub_name" {
  type        = string
  description = "The name of the AAS model data changed EventHub."
  default     = "aas-model-data-changed-eh"
}

variable "aas_telemetry_data_changed_eventhub_name" {
  type        = string
  description = "The name of the AAS telemetry data changed EventHub."
  default     = "aas-telemetry-data-changed-eh"
}

variable "adt_data_changed_eventhub_name" {
  type        = string
  description = "The name of the Digital Twins EventHub."
  default     = "adt-data-changed-eh"
}

# Function App variables
variable "function_app_storage_account_replication_type" {
  type        = string
  description = "The replication type of the function app's storage account."
  default     = "LRS"
}

variable "model_data_service_plan_sku_name" {
  type        = string
  description = "The tier and size of the model data service plan."
  default     = "Y1" # Elastic and Consumption SKUs (Y1, EP1, EP2, and EP3) are for use with Function Apps.
}

variable "model_data_maximum_elastic_worker_count" {
  type        = number
  description = "maximum number of elastic model data workers."
  default     = null
}

variable "telemetry_data_service_plan_sku_name" {
  type        = string
  description = "The tier and size of the telemetry data service plan."
  default     = "Y1" # Elastic and Consumption SKUs (Y1, EP1, EP2, and EP3) are for use with Function Apps.
}

variable "telemetry_data_maximum_elastic_worker_count" {
  type        = number
  description = "maximum number of elastic telemetry data workers."
  default     = null
}

variable "telemetry_data_elastic_instance_minimum" {
  type        = number
  description = "Minimum number of elastic telemetry data workers."
  default     = 1
}

# Function App App Setting variables
variable "shell_storage_path" {
  type        = string
  description = "The path to store the shells in the storage account."
  default     = "aas"
}

variable "abbreviated_company_name" {
  type        = string
  description = "The abbreviated company name."
}


variable "circuit_breaker_allowed_exception_count" {
  type        = number
  description = "The number of consecutive exceptions allowed before the circuit breaks for ADT SDK calls."
  default     = 3
}

variable "circuit_breaker_wait_time_sec" {
  type        = number
  description = "The duration of the circuit break (in seconds) after the number of max allowed exceptions is reached."
  default     = 60
}

# Storage account variables
variable "model_data_storage_container_name" {
  type        = string
  description = "The name of the model data storage container."
  default     = "model-data-container"
}

variable "model_data_storage_account_replication_type" {
  type        = string
  description = "The replication type of the model data storage account."
  default     = "GRS"
}

variable "function_storage_account_replication_type" {
  type        = string
  description = "The replication type of the funtion storage account."
  default     = "LRS"
}

variable "model_data_storage_account_retention_days" {
  type        = number
  description = "Number of days to keep the backups"
  default     = 90
}

variable "aas_metamodels_path" {
  type        = string
  description = "The path to the AAS metamodels."
  default     = "../adt-aas-models"
}