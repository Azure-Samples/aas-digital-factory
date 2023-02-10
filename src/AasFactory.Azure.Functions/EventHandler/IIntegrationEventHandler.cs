using AasFactory.Azure.Models.EventHubs;

namespace AasFactory.Azure.Functions.EventHandler;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    void Handle(TIntegrationEvent @event);
}