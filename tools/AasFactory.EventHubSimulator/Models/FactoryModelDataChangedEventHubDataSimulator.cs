using AasFactory.Azure.Models.EventHubs.Events.V1;

namespace AasFactory.EventHubSimulator.Models;

class FactoryModelDataChangedEventHubDataSimulator : BaseEventHubSimulatorData<FactoryModelDataEventChanged>
{
    public FactoryModelDataChangedEventHubDataSimulator()
    {
        this.EventType = typeof(FactoryModelDataEventChanged).FullName!;
    }
}