using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Request;
using AasFactory.Azure.Models.Factory.Enums;
using AasFactory.Azure.Models.Factory.StreamingData;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.EventHubSimulator;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


const string settingsFile = "local.settings.json";

Config BuildConfig()
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile(settingsFile, true)
        .AddEnvironmentVariables()
        .Build();

    var config = new Config(configuration);
    return config;
}

ModelDataRequest? GetBlob(string connectionString, string container, string path)
{
    var blobClient = new BlobClient(connectionString, container, path);
    var response = blobClient.DownloadContent();
    var stringifiedContent = response.Value.Content.ToString();
    return JsonConvert.DeserializeObject<ModelDataRequest>(stringifiedContent);
}

object? CreatePropertySample(DataType dataType)
{
    var random = new Random();
    switch (dataType)
    {
        case DataType.BigInt:
            return random.Next(100);
        case DataType.Boolean:
            return random.Next(2) == 1;
        case DataType.DateTime:
            return DateTime.UtcNow;
        case DataType.Float32:
            return random.NextDouble()*100;
        case DataType.Float64:
            return random.NextDouble()*100;
        case DataType.Int:
            return random.Next(100);
        case DataType.String:
            return $"sample-{DateTime.UtcNow}";
        default:
            Console.WriteLine($"The data type {dataType} does not have a converter and will not have a sample.");
            return null;
    }
}

IEnumerable<(MachineType machineType, Machine machine)> BuildMachineTypeAndMachinePair(ModelDataRequest modelData)
{
    var machineTypeMap = modelData.MachineType
        .ToDictionary(machineType => machineType.Id);
    
     var machineTypeAndMachineList = new List<(MachineType machineType, Machine machine)>();
    foreach (var machine in modelData.Machine)
    {
        if (!machineTypeMap.TryGetValue(machine.MachineType.Id, out var machineType))
        {
            continue;
        }

        machineTypeAndMachineList.Add((machineType, machine));
    }

    return machineTypeAndMachineList;
}

async Task Main(IEnumerable<(MachineType machineType, Machine machine)> machineTypesAndMachines, EventHubProducerClient producerClient, Config config)
{
    // traverse machines to get properties and create random property values
    var streamingDataPayload = new List<FactoryStreamingDataChanged>();
    foreach (var pair in machineTypesAndMachines)
    {
        var streamingDataChanged = new FactoryStreamingDataChanged();
        streamingDataChanged.Header.MachineId = pair.machine.Id;
        streamingDataChanged.Header.ModelType = ModelInstanceType.MachineType;

        var streamingDataFields = new List<StreamingDataField>();
        foreach(var field in pair.machineType.Fields)
        {
            var streamingDataField = new StreamingDataField()
            {
                Id = field.Id,
                Name = field.Name,
                DataType = field.DataType,
                Value = CreatePropertySample(field.DataType),
            };
            streamingDataFields.Add(streamingDataField);
        }

        streamingDataChanged.Data = streamingDataFields;
        streamingDataPayload.Add(streamingDataChanged);
    }

    // Sending the events
    Console.WriteLine("Building the list of events to send...");
    var eventDataBatch = new List<EventData>();
    foreach (var integrationEvent in streamingDataPayload)
    {
        var eventBody = JsonConvert.SerializeObject(integrationEvent);
        var eventData = new EventData(eventBody);
        eventData.Properties.Add("EventType", typeof(FactoryStreamingDataChanged).FullName!);
        eventDataBatch.Add(eventData);
    }

    try
    {
        // Use the producer client to send the batch of events to the event hub
        await producerClient.SendAsync(eventDataBatch);
        Console.WriteLine($"{eventDataBatch.Count()} events sent to {config.EventHubName}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
}

Console.WriteLine("Reading config...");
var config = BuildConfig();

// Create a producer client that you can use to send events to an event hub
var producerClient = new EventHubProducerClient(config.EventHubConnectionString, config.EventHubName);

Console.WriteLine("Reading blob from blob storage...");
var modelData = GetBlob(config.BlobStorageConnectionString, config.BlobStorageContainer, config.BlobStorageBlobPath);
if (modelData is null)
{
    Console.WriteLine("No model data to create simulated data on... Exiting program.");
    return;
}

var machineTypesAndMachines = BuildMachineTypeAndMachinePair(modelData);
var timer = new PeriodicTimer(TimeSpan.FromSeconds(config.TimeBetweenEventsInSeconds));
while (await timer.WaitForNextTickAsync())
{
    await Main(machineTypesAndMachines, producerClient, config);
}