using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Logger;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow.EventHandler;

public class AasStreamingDataChangedHandlerAsync : IIntegrationEventHandlerAsync<AasStreamingDataChanged>
{
    private readonly ILogger<AasStreamingDataChangedHandlerAsync> logger;
    private readonly IPropertyService propertyService;

    public AasStreamingDataChangedHandlerAsync(ILogger<AasStreamingDataChangedHandlerAsync> logger, IPropertyService propertyService)
    {
        this.logger = logger;
        this.propertyService = propertyService;
    }

    public async Task Handle(AasStreamingDataChanged eventData)
    {
        // Extract the cycle start time to add as metadata to each of the property events in this payload
        var startTimeProperty = eventData.Properties
            .First(p =>
                string.Equals(p.IdShort, eventData.SourceTimestampFieldName, StringComparison.InvariantCultureIgnoreCase));

        // Update the property values/metadata for each property event in the payload
        var startTime = startTimeProperty.Value;
        foreach (var property in eventData.Properties)
        {
            await this.propertyService.UpdatePropertyValues(property!, startTime!);
        }

        this.logger.ProcessedAasToAdtStreamingData();
    }
}