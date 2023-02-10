using AasFactory.Azure.Models.EventHubs;

namespace AasFactory.Azure.Functions.EventHandler;

public interface IIntegrationEventHandlerWithReturn<in TIntegrationEvent, TReturn> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    TReturn Handle(TIntegrationEvent @event);
}