using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.Functions;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Functions;

[ExcludeFromCodeCoverage]
public class FactoryStreamingDataChangedFunction : BasicFunctionWithReturn<EventData?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FactoryStreamingDataChangedFunction"/> class.
    /// </summary>
    /// <param name="serviceProvider">A service provider.</param>
    /// <param name="logger">A category logger.</param>
    public FactoryStreamingDataChangedFunction(
        IServiceProvider serviceProvider, ILogger<FactoryStreamingDataChangedFunction> logger)
        : base(serviceProvider, logger)
    {
    }

    /// <summary>
    /// Translates the data to AAS to be sent to either ADT or ADX processing eventhubs.
    /// </summary>
    /// <param name="eventData"></param>
    /// <param name="adtCollector"></param>
    /// <param name="adxCollector"></param>
    /// <returns></returns>
    [FunctionName(nameof(FactoryStreamingDataChangedFunction))]
    [return: EventHub("%AAS_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")]
    public EventData? Run([EventHubTrigger("%FACTORY_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData)
    {
        var outputEventData = this.RunFunctionWithReturn(eventData);
        return outputEventData;
    }

    /// <summary>
    /// The default version for the incoming payloads is FactoryStreamingDataChanged v1.
    /// </summary>
    /// <returns>default version for incomming payloads</returns>
    protected override Type? GetDefaultIntegrationEvent()
    {
        return typeof(Models.EventHubs.Events.V1.FactoryStreamingDataChanged);
    }
}