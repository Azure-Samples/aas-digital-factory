using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.Logger;
using AasFactory.Azure.Models.EventHubs;
using AasFactory.Azure.Models.EventHubs.Extensions;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.Functions
{
    public abstract class BasicFunction
    {
        public BasicFunction(IServiceProvider serviceProvider, ILogger logger)
        {
            this.Logger = logger;
            this.ServiceProvider = serviceProvider;
        }

        protected ILogger Logger { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        public void RunFunction(EventData eventData)
        {
            try
            {
                this.PopulateEventType(eventData);
                var handlerInfo = this.GetHandlerInfo(eventData);

                var state = GetState(handlerInfo);

                using (this.Logger.BeginScope(state))
                {
                    var methodInfo = handlerInfo.Handler.GetType().GetMethod("Handle")!;
                    methodInfo.Invoke(handlerInfo.Handler, new object[] { handlerInfo.IntegrationEvent });
                }
            }
            catch (Exception e)
            {
                // We need to keep processing the rest of the batch - capture this exception and continue.
                // Also, consider capturing details of the message that failed processing so it can be processed again later.
                this.Logger.FailedToProcessFunction(e);
                throw;
            }
        }

        protected static Dictionary<string, object> GetState((IIntegrationEventBasicHandler Handler, IntegrationEvent IntegrationEvent) handlerInfo)
        {
            return new Dictionary<string, object>
            {
                ["trace_id"] = handlerInfo.IntegrationEvent.MetaData.TraceId,
                ["event_type"] = handlerInfo.IntegrationEvent.GetType(),
            };
        }

        protected (IIntegrationEventBasicHandler Handler, IntegrationEvent IntegrationEvent) GetHandlerInfo(EventData eventData)
        {
            var eventTypeValue = eventData.TryGetEvent();

            if (eventTypeValue == null)
            {
                throw new Exception($"There is no event with the type of {eventData.GetEventType()}");
            }

            IIntegrationEventBasicHandler? eventHandler = this.GetInterfaceHandler(eventData, eventTypeValue);

            return (eventHandler, eventTypeValue);
        }

        protected virtual Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue, EventData eventData)
        {
            var typeOfHandler = typeof(IIntegrationEventHandler<>);
            var integrationEventHandler = typeOfHandler.MakeGenericType(eventTypeValue.GetType());
            return integrationEventHandler;
        }

        protected virtual Type? GetDefaultIntegrationEvent()
        {
            return null;
        }

        protected void PopulateEventType(EventData eventData)
        {
            if (eventData.Properties.ContainsKey("EventType"))
            {
                return;
            }

            var defaultType = this.GetDefaultIntegrationEvent();

            if (defaultType is null)
            {
                return;
            }

            eventData.Properties["EventType"] = defaultType.ToString();
        }

        private IIntegrationEventBasicHandler GetInterfaceHandler(EventData eventData, IntegrationEvent? eventTypeValue)
        {
            var integrationEventHandler = this.GetInterfaceHandlerType(eventTypeValue!, eventData);
            var eventHandler = this.ServiceProvider.GetService(integrationEventHandler)! as IIntegrationEventBasicHandler;

            if (eventHandler == null)
            {
                throw new Exception($"There is no registered handler to process {integrationEventHandler}");
            }

            return eventHandler;
        }
    }
}
