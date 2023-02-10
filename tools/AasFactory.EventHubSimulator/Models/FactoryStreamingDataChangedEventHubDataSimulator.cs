using AasFactory.Azure.Models.EventHubs.Events.V1;

namespace AasFactory.EventHubSimulator.Models;

class FactoryStreamingDataChangedEventHubDataSimulator : BaseEventHubSimulatorData<FactoryStreamingDataChanged>
{
    public FactoryStreamingDataChangedEventHubDataSimulator()
    {
        this.EventType = typeof(FactoryStreamingDataChanged).FullName!;
    }
}