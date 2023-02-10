output "function_app_principal_id" {
  value       = azurerm_linux_function_app.function_app.identity[0].principal_id
  description = "The service principal id of the function app."
}
