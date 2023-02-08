using AasFactory.EventHubSimulator;
using AasFactory.EventHubSimulator.Models;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

const string settingsFile = "local.settings.json";

Config BuildConfig()
{
    if (!File.Exists(settingsFile))
    {
        throw new Exception($"{settingsFile} does not exist. Please add {settingsFile} and retry.");
    }

    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile(settingsFile)
        .AddEnvironmentVariables()
        .Build();

    var config = new Config(configuration);
    return config;
}

Console.WriteLine("Reading config...");
var config = BuildConfig();
var integrationInstanceStringified = File.ReadAllText(config.EventDetailsFile);

// setting the integration events and event type
IntegrationEventEventHubSimulatorData? integrationEventData;
switch(config.EventType)
{
    case EventType.FactoryModelDataEventChangedV1:
        integrationEventData = JsonConvert.DeserializeObject<FactoryModelDataChangedEventHubDataSimulator>(integrationInstanceStringified)?.Convert();
        break;
    case EventType.AasModelDataEventChangedV1:
        integrationEventData = JsonConvert.DeserializeObject<AasModelDataChangedEventHubDataSimulator>(integrationInstanceStringified)?.Convert();
        break;
    case EventType.FactoryStreamingDataEventChangedV1:
        integrationEventData = JsonConvert.DeserializeObject<FactoryStreamingDataChangedEventHubDataSimulator>(integrationInstanceStringified)?.Convert();
        break;
    case EventType.AasStreamingDataEventChangedV1:
        integrationEventData = JsonConvert.DeserializeObject<AasStreamingDataChangedEventHubDataSimulator>(integrationInstanceStringified)?.Convert();
        break;
    default:
        throw new Exception($"event type {config.EventType} is not supported.");
}

if (integrationEventData is null)
{
    throw new Exception($"The file {config.EventDetailsFile} is empty.");
}

Console.WriteLine("Finished reading the config.");

// Create a producer client that you can use to send events to an event hub
var producerClient = new EventHubProducerClient(config.EventHubConnectionString, integrationEventData.EventHubName);

// creating the batch of events
Console.WriteLine("Building the list of events to send...");
var eventDataBatch = new List<EventData>();
foreach (var integrationEvent in integrationEventData.Data)
{
    var eventBody = JsonConvert.SerializeObject(integrationEvent);
    var eventData = new EventData(eventBody);
    eventData.Properties.Add("EventType", integrationEventData.EventType);
    eventDataBatch.Add(eventData);
}

try
{
    // Use the producer client to send the batch of events to the event hub
    await producerClient.SendAsync(eventDataBatch);
    Console.WriteLine($"{eventDataBatch.Count()} events sent to {integrationEventData.EventHubName}.");
}
catch (Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}
