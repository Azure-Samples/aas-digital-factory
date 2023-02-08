# Event Hub Simulator <!-- omit in toc -->

This program sends an event to a specific Azure Function in Model Data Flow or Streaming Data Flow.
The use case of this program is to send an event to an individual function.
For streaming constant events to the streaming data flow, the `AasFactory.StreamingSimulator` program is a better solution.

## Sections <!-- omit in toc -->

- [Configuration](#configuration)
  - [local.settings.json](#localsettingsjson)
  - [Factory Model Data Event Changed V1 Config](#factory-model-data-event-changed-v1-config)
  - [AAS Model Data Event Changed V1 Config](#aas-model-data-event-changed-v1-config)
  - [Factory Streaming Data Event Changed V1 Config](#factory-streaming-data-event-changed-v1-config)
  - [AAS Streaming Data Event Changed V1 Config](#aas-streaming-data-event-changed-v1-config)
- [Run the Azure Functions locally](#run-the-azure-functions-locally)
  - [Steps](#steps)

## Configuration

### local.settings.json

- **EVENT_HUB_CONNECTION_STRING** [REQUIRED]: The connection string for Azure Event Hub.

- **EVENT_DETAILS_FILE** [REQUIRED]: The file that contains the list of events that will be sent to the Event Hub.

- **EVENT_TYPE** [REQUIRED]: The type of event that will is being sent to Event Hub. The event type can be one of the following:

  - **1**: Factory Model Data Event Changed V1
  - **2**: AAS Model Data Event Changed V1
  - **3**: Factory Streaming Data Event Changed V1
  - **4**: AAS Streaming Data Event Changed V1

    **NOTE**: The event type should be a number and should **not** be surrounded in quotes.

### Factory Model Data Event Changed V1 Config

The file for `Factory Model Data Event Changed V1` should contain the following content.

```json
{
  "EventHubName": "<event_hub_name>",
  "Data": [
    {
        "Path": "aas/<workspace>.json",
        "OutputFileName": "<workspace>",
    }
  ]
}
```

Where:

- **EventHubName** [REQUIRED]: The name of the Event Hub where the event will be sent.
- **Data[0].Path** [REQUIRED]: The path to the AAS blob in storage. This path should not include the container name.
- **Data[0].OutputFileName** [REQUIRED]: the output file name in the shell directory. This file name should not include the extension (ex: `output` instead of `output.json`).

**NOTE**: This payload should only have one event.

### AAS Model Data Event Changed V1 Config

The file for `AAS Model Data Event Changed V1` should contain the following content.

```json
{
  "EventHubName": "<evnet_hub_name>",
  "Data": [
    {
        "Path": "aas/<workspace>.json",
    }
  ]
}
```

Where:

- **EventHubName** [REQUIRED]: The name of the Event Hub where the event will be sent.
- **Data[0].Path** [REQUIRED]: The path to the AAS blob in storage. This path should not include the container name.

**NOTE**: This payload should only have one event.

### Factory Streaming Data Event Changed V1 Config

The file for `Factory Streaming Data Event Changed V1` should contain the following content.

```json
{
  "EventHubName": "<event_hub_name>",
  "Data": [
    {
      "Header": {
        "MachineId": "<machine_id>",
        "ModelType": "MachineType"
      },
      "Data": [
        {
          "Id": "<field id>",
          "Name": "<name of field>",
          "DataType": "<data type>",
          "Value": "<new value of the twin>"
        },
        ...
      ]
    }
  ]
}
```

Where:

- **EventHubName** [REQUIRED]: The name of the Event Hub where the event will be sent.
- **Data[\*].Header.MachineId** [REQUIRED]: The id of the machine.
- **Data[\*].Header.ModelType** [REQUIRED]: The model type (or type of streaming data) of the telemetry data. For now this should always be `MachineType`.
- **Data[\*].Properties[\*].Id** [REQUIRED]: The initial id of the property.
- **Data[\*].Properties[\*].Name** [REQUIRED]: The name of the property.
- **Data[\*].Properties[\*].DataType** [REQUIRED]: The data type of the property.
- **Data[\*].Properties[\*].Value** [REQUIRED]: The new telemetry value.

### AAS Streaming Data Event Changed V1 Config

The file for `AAS Streaming Data Event Changed V1` should contain the following content.

```json
{
  "EventHubName": "<evnet_hub_name>",
  "Data": [
    {
      "SourceTimestampFieldName": "<source timestamp field name>",
      "Properties": [
        {
          "Id": "<dtid of property>",
          "IdShort": "<name of property>",
          "ValueType": 0, // value type enum number of the property
          "Value": "<new value of the twin>"
        },
        ...
      ]
    }
  ]
}
```

Where:

- **EventHubName** [REQUIRED]: The name of the Event Hub where the event will be sent.
- **Data[\*].SourceTimestampFieldName** [REQUIRED]: The field name that corresponds to the source timestamp of the property.
- **Data[\*].Properties[\*].Id** [REQUIRED]: The dtId of the property that will be updated.
- **Data[\*].Properties[\*].IdShort** [REQUIRED]: The name of the property.
- **Data[\*].Properties[\*].ValueType** [REQUIRED]: The value type of the property.
  More information on the values of the value type enum can be found [here](../../src/AasFactory.Azure.Models/Aas/Metamodels/Enums/PropertyType.cs).
- **Data[\*].Properties[\*].Value** [REQUIRED]: The new value to update the twin with.

## Run the Azure Functions locally

### Steps

To copy start with the sample configuration:

```bash
cp sample.local.settings.json local.settings.json
```

To run the Program:

```bash
dotnet run
```
