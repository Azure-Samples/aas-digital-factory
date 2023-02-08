using AasFactory.Azure.Models.EventHubs.Events.V1;

namespace AasFactory.EventHubSimulator.Models;

class AasModelDataChangedEventHubDataSimulator : BaseEventHubSimulatorData<AasModelDataEventChanged>
{
    public AasModelDataChangedEventHubDataSimulator()
    {
        this.EventType = typeof(AasModelDataEventChanged).FullName!;
    }
}