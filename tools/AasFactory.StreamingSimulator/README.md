# Streaming Simulator <!-- omit in toc -->

This program sends a constant stream of random event values to the `FactoryStreamingDataChangedFunction` function based on an input factory definition.
[Factory.json](../../samples/model-data/Factory.json) is an example of an input factory definition.
This program reads this definition from blob storage and creates new telemetry samples for all machine instances.
The configuration in [local.settings.json](#localsettingsjson) controls how often the events are sent to the `FactoryStreamingDataChangedFunction` function.

## Sections <!-- omit in toc -->

- [Configuration](#configuration)
  - [local.settings.json](#localsettingsjson)
- [Run locally](#run-locally)
  - [Steps](#steps)

## Configuration

### local.settings.json

- **EVENT_HUB_CONNECTION_STRING** [REQUIRED]: The connection string for Azure Event Hub.

- **EVENT_HUB_NAME** [REQUIRED]: The name of the event hub the events will go to.

- **BLOB_STORAGE_CONNECTION_STRING** [REQUIRED]: The connection string to blob storage.

- **BLOB_STORAGE_CONTAINER** [REQUIRED]: The storage container that contains the target blob.

- **BLOB_STORAGE_BLOB_PATH** [REQUIRED]: The path to the raw factory definition file in the storage container.
  
- **TIME_BETWEEN_EVENTS_IN_SECONDS** [DEFAULT: 30]: The amount of time between streaming events in seconds.

## Run locally

### Steps

To copy start with the sample configuration:

```bash
cp sample.local.settings.json local.settings.json
```

Please ensure the `local.settings.json` file contians the correct values after copying the file.

To run the Program:

```bash
dotnet run
```
