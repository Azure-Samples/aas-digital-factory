output "model_data_function_app_name" {
  value       = local.model_data_function_app_name
  description = "The name of the model data flow azure function."
}

output "telemetry_data_function_app_name" {
  value       = local.telemetry_data_function_app_name
  description = "The name of the telemetry data flow azure function."
}

output "model_data_storage_account_name" {
  value       = local.model_data_storage_account_name
  description = "The name of the mode data storage account."
}

output "model_data_storage_container_name" {
  value       = var.model_data_storage_container_name
  description = "The name of the model data storage container."
}