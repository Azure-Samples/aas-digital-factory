using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.Functions;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow;

[ExcludeFromCodeCoverage]
public class AasStreamingDataChangedFunction : BasicFunctionAsync
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AasStreamingDataChangedFunction"/> class.
    /// </summary>
    /// <param name="serviceProvider">A service provider.</param>
    /// <param name="loggerFactory">A logger factory.</param>
    public AasStreamingDataChangedFunction(
        IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
        : base(serviceProvider, loggerFactory.CreateLogger<AasStreamingDataChangedFunction>())
    {
    }

    /// <summary>
    /// Updates the data for properties in ADT based on the AAS EventHub data.
    /// </summary>
    /// <param name="events">The input EventHub events.</param>
    /// <returns>A task that represents completeness.</returns>
    [FunctionName(nameof(AasStreamingDataChangedFunction))]
    public async Task Run([EventHubTrigger("%AAS_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData)
    {
        await this.RunFunctionAsync(eventData);
    }
}