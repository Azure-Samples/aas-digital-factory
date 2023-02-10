using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.Functions;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.ModelDataFlow.Functions;

[ExcludeFromCodeCoverage]
public class FactoryModelDataChangedFunction : BasicFunctionWithReturn<EventData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FactoryModelDataChangedFunction"/> class.
    /// </summary>
    /// <param name="serviceProvider">A service provider.</param>
    /// <param name="loggerFactory">A logger factory.</param>
    public FactoryModelDataChangedFunction(
        IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
        : base(serviceProvider, loggerFactory.CreateLogger<FactoryModelDataChangedFunction>())
    {
    }

    /// <summary>
    /// Converts the data to AAS.
    /// </summary>
    /// <param name="eventData">The input eventhub message.</param>
    /// <returns>An event hub message to trigger the function to convert AAS to Adt.</returns>
    [FunctionName(nameof(FactoryModelDataChangedFunction))]
    [return: EventHub("%AAS_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")]
    public EventData Run([EventHubTrigger("%FACTORY_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData)
    {
        var outputEventData = this.RunFunctionWithReturn(eventData);
        return outputEventData;
    }

    /// <summary>
    /// For model data received, the default version for the incoming payloads is v1.
    /// </summary>
    /// <returns>default version for incomming payloads</returns>
    protected override Type? GetDefaultIntegrationEvent()
    {
        return typeof(Models.EventHubs.Events.V1.FactoryModelDataEventChanged);
    }
}