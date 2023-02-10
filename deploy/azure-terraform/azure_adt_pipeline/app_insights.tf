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

resource "azurerm_monitor_action_group" "main" {
  name                = "emailalerts-actiongroup"
  resource_group_name = var.resource_group_name
  short_name          = "emailalerts"

  email_receiver {
    name          = "sendtoops"
    email_address = var.alert_action_group_email_address
  }
}

// Note default values per TF: severity = 3, frequency of evaluation = PT1M (1 minute),
// window of evaluation = PT5M (5 minutes)
resource "azurerm_monitor_metric_alert" "metricalert_exceptioncount_modeldataflow" {
  name                = "Alert - Model Data Flow Function Exceptions"
  resource_group_name = var.resource_group_name
  scopes              = [azurerm_application_insights.app_insights.id]
  description         = "Email notification will be sent when ${var.model_data_function_app_name} Exception count is greater than ${var.mdf_alert_exception_threshold}."
  severity            = var.mdf_alert_severity

  criteria {
    metric_namespace = "Microsoft.Insights/components"
    metric_name      = "exceptions/count"
    aggregation      = "Count"
    operator         = "GreaterThan"
    threshold        = var.mdf_alert_exception_threshold

    dimension {
      name     = "cloud/roleName"
      operator = "Include"
      values   = [var.model_data_function_app_name]
    }
  }

  action {
    action_group_id = azurerm_monitor_action_group.main.id
  }
}

resource "azurerm_monitor_metric_alert" "metricalert_exceptioncount_telemetrydataflow" {
  name                = "Alert - Streaming Data Flow Function Exceptions"
  resource_group_name = var.resource_group_name
  scopes              = [azurerm_application_insights.app_insights.id]
  description         = "Email notification will be sent when ${var.telemetry_data_function_app_name} Exception count is greater than ${var.tdf_alert_exception_threshold}."
  severity            = var.tdf_alert_severity

  criteria {
    metric_namespace = "Microsoft.Insights/components"
    metric_name      = "exceptions/count"
    aggregation      = "Count"
    operator         = "GreaterThan"
    threshold        = var.tdf_alert_exception_threshold

    dimension {
      name     = "cloud/roleName"
      operator = "Include"
      values   = [var.telemetry_data_function_app_name]
    }
  }

  action {
    action_group_id = azurerm_monitor_action_group.main.id
  }
}