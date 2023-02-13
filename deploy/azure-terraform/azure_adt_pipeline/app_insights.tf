resource "azurerm_log_analytics_workspace" "log_analytics_workspace" {
  name                = var.log_analytics_workspace_name
  location            = var.location
  resource_group_name = var.resource_group_name
  retention_in_days   = var.log_analytics_workspace_retention_in_days

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_application_insights" "app_insights" {
  name                = var.app_insights_name
  location            = var.location
  resource_group_name = var.resource_group_name
  workspace_id        = azurerm_log_analytics_workspace.log_analytics_workspace.id
  application_type    = "web"

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}
