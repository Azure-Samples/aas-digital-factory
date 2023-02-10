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

variable "storage_account_name" {
  type        = string
  description = "The name of the function's storage account."
}

variable "storage_account_replication_type" {
  type        = string
  description = "The replication type for the storage account (LRS,GRS,etc.)."
}

variable "service_plan_name" {
  type        = string
  description = "The name of the function's service plan."
}

variable "service_plan_sku_name" {
  type        = string
  description = "The sku name of the service plan."
}

variable "maximum_elastic_worker_count" {
  type        = number
  description = "The maximum number of elastic workers."
}

variable "elastic_instance_minimum" {
  type        = number
  description = "The minimum number of warmed up elastic instances."
}

variable "function_app_name" {
  type        = string
  description = "The name of the function app."
}

variable "function_app_app_settings" {
  type        = map(string)
  description = "The function app's app settings."
}

variable "app_insights_connection_string" {
  type        = string
  description = "The connection string for applicaiton insights."
}

variable "app_insights_instrumentation_key" {
  type        = string
  description = "The instrumentation key for application insights."
}