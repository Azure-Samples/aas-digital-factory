using AasFactory.Azure.Models.EventHubs.Events.V1;

namespace AasFactory.EventHubSimulator.Models;

class AasStreamingDataChangedEventHubDataSimulator : BaseEventHubSimulatorData<AasStreamingDataChanged>
{
    public AasStreamingDataChangedEventHubDataSimulator()
    {
        this.EventType = typeof(AasStreamingDataChanged).FullName!;
    }
}