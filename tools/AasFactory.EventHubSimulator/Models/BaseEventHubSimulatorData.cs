using AasFactory.Azure.Models.EventHubs;

namespace AasFactory.EventHubSimulator.Models;

class BaseEventHubSimulatorData<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// Gets or sets the event hub name.
    /// </summary>
    public string EventHubName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the event type.
    /// </summary>
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public IEnumerable<TIntegrationEvent> Data { get; set; } = Enumerable.Empty<TIntegrationEvent>();

    public IntegrationEventEventHubSimulatorData Convert()
    {
        var integrationEventEnventHubSimulatorData = new IntegrationEventEventHubSimulatorData();
        integrationEventEnventHubSimulatorData.Data = this.Data;
        integrationEventEnventHubSimulatorData.EventHubName = this.EventHubName;
        integrationEventEnventHubSimulatorData.EventType = this.EventType;

        return integrationEventEnventHubSimulatorData;
    }
}