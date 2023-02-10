using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.Logger;
using AasFactory.Azure.Models.EventHubs;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.Functions
{
    public class BasicFunctionAsync : BasicFunction
    {
        public BasicFunctionAsync(IServiceProvider serviceProvider, ILogger logger)
            : base(serviceProvider, logger)
        {
        }

        public async Task RunFunctionAsync(EventData eventData)
        {
            try
            {
                this.PopulateEventType(eventData);
                var handlerInfo = this.GetHandlerInfo(eventData);
                Dictionary<string, object> state = GetState(handlerInfo);

                using (this.Logger.BeginScope(state))
                {
                    var methodInfo = handlerInfo.Handler.GetType().GetMethod("Handle")!;
                    var task = (Task)methodInfo.Invoke(handlerInfo.Handler, new object[] { handlerInfo.IntegrationEvent })!;
                    await task;
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

        protected override Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue, EventData eventData)
        {
            var typeOfHandler = typeof(IIntegrationEventHandlerAsync<>);
            var integrationEventHandler = typeOfHandler.MakeGenericType(eventTypeValue.GetType());
            return integrationEventHandler;
        }
    }
}
