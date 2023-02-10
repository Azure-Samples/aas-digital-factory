using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Logger;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.EventHubs.Extensions;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AasFactory.Azure.Functions.StreamingDataFlow.EventHandler;

/// <summary>
/// The processor converts the incoming payload to AAS
/// </summary>
public class FactoryStreamingDataChangedHandler : IIntegrationEventHandlerWithReturn<FactoryStreamingDataChanged, EventData?>
{
    private readonly ILogger<FactoryStreamingDataChangedHandler> logger;
    private readonly IFactoryStreamingDataService streamingDataService;
    private readonly IStreamingDataFlowSettings settings;

    public FactoryStreamingDataChangedHandler(
        IFactoryStreamingDataService streamingDataService,
        IStreamingDataFlowSettings settings,
        ILogger<FactoryStreamingDataChangedHandler> logger)
    {
        this.logger = logger;
        this.streamingDataService = streamingDataService;
        this.settings = settings;
    }

    public EventData? Handle(FactoryStreamingDataChanged eventData)
    {
        var aasStreamingData = this.streamingDataService.ProcessStreamingData(eventData);
        if (aasStreamingData == null)
        {
            return null!;
        }

        // ADT patch event data
        this.logger.FactoryStreamingDataAdtEventProcessing();
        var eventBodyStringified = JsonConvert.SerializeObject(aasStreamingData);
        var returnedEventData = new EventData(eventBodyStringified);

        var outputEventType = typeof(AasStreamingDataChanged);
        returnedEventData.SetEventType(outputEventType.FullName!);
        returnedEventData.CorrelationId = eventData.MetaData.TraceId;
        this.logger.FactoryStreamingDataAdtEventProcessingDone();
        return returnedEventData;
    }
}