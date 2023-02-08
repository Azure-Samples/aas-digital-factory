using AasFactory.Azure.Models.EventHubs;

namespace AasFactory.Azure.Functions.EventHandler;

public interface IIntegrationEventHandlerAsync<in TIntegrationEvent> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}