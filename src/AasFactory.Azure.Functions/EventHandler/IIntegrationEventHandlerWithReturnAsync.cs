using AasFactory.Azure.Models.EventHubs;

namespace AasFactory.Azure.Functions.EventHandler;

public interface IIntegrationEventHandlerWithReturnAsync<in TIntegrationEvent, TReturn> : IIntegrationEventBasicHandler
        where TIntegrationEvent : IntegrationEvent
{
    Task<TReturn> Handle(TIntegrationEvent @event);
}