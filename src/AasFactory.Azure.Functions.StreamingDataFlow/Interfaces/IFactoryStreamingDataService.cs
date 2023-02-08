using AasFactory.Azure.Models.EventHubs;
using AasFactory.Azure.Models.EventHubs.Events.V1;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;

/// <summary>
/// Abstraction of streaming data service to process streaming data.
/// </summary>
public interface IFactoryStreamingDataService
{
    /// <summary>
    /// Processes streaming data.
    /// </summary>
    /// <param name="streamingData">The streaming data</param>
    /// <returns>Integration event</returns>
    IntegrationEvent? ProcessStreamingData(FactoryStreamingDataChanged streamingData);
}
