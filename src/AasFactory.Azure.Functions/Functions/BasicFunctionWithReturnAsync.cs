using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.Logger;
using AasFactory.Azure.Models.EventHubs;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.Functions
{
    public abstract class BasicFunctionWithReturnAsync<TReturn> : BasicFunction
    {
        public BasicFunctionWithReturnAsync(IServiceProvider serviceProvider, ILogger logger)
            : base(serviceProvider, logger)
        {
        }

        public virtual async Task<TReturn> RunFunctionWithReturnAsync(EventData eventData)
        {
            try
            {
                this.PopulateEventType(eventData);
                var handlerInfo = this.GetHandlerInfo(eventData);
                var state = BasicFunction.GetState(handlerInfo);

                using (this.Logger.BeginScope(state))
                {
                    var methodInfo = handlerInfo.Handler.GetType().GetMethod("Handle")!;
                    var task = (Task<TReturn>)methodInfo.Invoke(handlerInfo.Handler, new object[] { handlerInfo.IntegrationEvent })!;
                    return await task;
                }
            }
            catch (Exception e)
            {
                this.Logger.FailedToProcessFunction(e);
                throw;
            }
        }

        protected override Type GetInterfaceHandlerType(IntegrationEvent eventTypeValue, EventData eventData)
        {
            var typeOfHandler = typeof(IIntegrationEventHandlerWithReturnAsync<,>);
            var integrationEventHandler = typeOfHandler.MakeGenericType(eventTypeValue.GetType(), typeof(TReturn));
            return integrationEventHandler;
        }
    }
}
