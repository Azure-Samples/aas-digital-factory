# Alerting mechanisms to monitor Azure Functions

- [Alerting mechanisms to monitor Azure Functions](#alerting-mechanisms-to-monitor-azure-functions)
  - [Overview](#overview)
  - [Best practices](#best-practices)
    - [Alert Rule Types](#alert-rule-types)
    - [Alert Severity](#alert-severity)
    - [Minimize Alert Activity](#minimize-alert-activity)
  - [Azure Monitor alert rules](#azure-monitor-alert-rules)
    - [Manual creation](#manual-creation)
    - [Terraform](#terraform)
  - [Integration with Azure Logic Apps](#integration-with-azure-logic-apps)

## Overview

This document goes over the process to configure and maintain alerts for abnormal Azure Functions behavior, based on metrics like exception count.
Alerts are meant to quickly identify and address any issues as soon as they arise in order to minimize the impact on end users.
[Azure Monitor alert rules](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview) are highly customizable,
with possible conditions based on any of the log or metric data ingested into Azure Monitor.
The current implementation reviewed in this document is intended as just an initial example
that can be extended based on future monitoring requirements for this project.

The error handling behaviors of both Azure Function projects (model data and streaming data flows) are described in the individual project READMEs.

## Best practices

Microsoft provides a great document on alerting and automated actions.
This section will just be a summary of a few of the key components from their document.
For a more detailed view into the alerting and automated actions in Azure please review [this document](https://learn.microsoft.com/en-us/azure/azure-monitor/best-practices-alerts).

### Alert Rule Types

There are four different alert rule types, each type has it's own capability and cost associated with it.

- [Activity log rule](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/activity-log-alerts)
- [Metric alert rule](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-metric-overview)
- [Log alert rule](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-unified-log)
- [Application alerts](https://learn.microsoft.com/en-us/azure/azure-monitor/app/monitor-web-app-availability)

### Alert Severity

Alert severity defined how urgent an issue is within the system.
There are 5 severity levels:

| Level | Name          | Description                                                                         |
|-------|---------------|-------------------------------------------------------------------------------------|
| Sev 0 | Critical      | Anything that requires immediate attention                                          |
| Sev 1 | Error         | Anything that requires attention but not immediate                                  |
| Sev 2 | Warning       | Attention is not required, but if not addressed, this could lead to future problems |
| Sev 3 | Informational | Provides interesting information to an operator. No underlying problem.             |
| Sev 4 | Verbose       | Detailed information about the system that does not pertain to erroneous behavior   |

### Minimize Alert Activity

While alerting is important, ensure that the alerts that are setup are essential and don't overwhelm administrators.
For more information and guidance on minimizing alerts, follow Microsoft's guidance on minimizing alert activity [here](https://learn.microsoft.com/en-us/azure/azure-monitor/best-practices-alerts#minimize-alert-activity).

## Azure Monitor alert rules

In general, an Azure Monitor alert rule gets triggered when the telemetry for a specified resource meets some **alert condition** set in the rule.
Alerts are connected to **action groups**, which specify the response when a rule is triggered:
these can include notifications (email, SMS, etc.) or automated workflows (Azure Functions, Logic Apps, Event Hubs, webhooks, etc.).
For the purposes of this project, we are using email notifications.

There are several kinds of alerts available in Azure Monitor: metric alerts, log alerts, activity log alerts, and Prometheus alerts.
[Metric alerts](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-types#metric-alerts) rely on available pre-computed data;
metric alerts are preferable to use vs. log alerts when the alerts don't need complex filtering or logic
as metric alerts often cost a lot less than log alerts.
The [terraform](#terraform) section has guidance on setting up metric alerts.
Log alerts follow a very similar process to the process outlined for the metric alerts.
**Dimensions** can be used to specify a particular time-series of telemetry data to monitor
(e.g. if you want to select a particular Azure Function or other resource).

### Manual creation

Follow steps 1 to 15 in [Create a new alert rule in the Azure Portal](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-create-new-alert-rule?tabs=metric#create-a-new-alert-rule-in-the-azure-portal).

### Terraform

The `azurerm_monitor_metric_alert` resource can be used to create an Azure Monitor metric alert rule using the Azure Terraform provider -
see [the Terraform docs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert.html#example-usage)
for more information.
Note the use of the `azurerm_monitor_action_group` resource (docs [here](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_action_group))
to specify the details of the action group: for email notifications, the `email_receiver` block is used.

For a sample Terraform implementation of metric rule that triggers an email notification based on the exception count from the model data Azure Function,
see [this file](../../deploy/azure-terraform/azure_adt_pipeline/app_insights.tf).
The Azure Function is already linked to our Application Insights instance via its app settings;
this alert rule can be specified to a particular time series by setting the scope to the ID of the App Insights instance
and the dimension `cloud/roleName` to the name of the function app.

As discussed in the Terraform docs linked above, there are some default values Terraform sets for alert parameters:

- Severity: 3
- Frequency (evaluation frequency for the metric): 1 min. (`PT1M`)
- Window size (time period over which the alert is assessed): 5 min. (`PT5M`)

In our implementation, we use their defaults for frequency and window size, but have made the severity of the model data flow alert 1 due to its significance
on the ADT population process.
All of these values can be parameterized and further configured if needed.

## Integration with Azure Logic Apps

Azure Logic Apps allows you to build and customize workflows for integration. Use Logic Apps to customize your alert notifications.

- Customize the alerts email, using your own email subject and body format.
- Customize the alert metadata by looking up tags for affected resources or fetching a log query search result.
- Integrate with external services using existing connectors like Outlook, Microsoft Teams, Slack and PagerDuty, or
by configuring the Logic App for your own services.

Article for [creating a Logic App and creating an action group](https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-logic-apps?tabs=send-email#create-a-logic-app).
