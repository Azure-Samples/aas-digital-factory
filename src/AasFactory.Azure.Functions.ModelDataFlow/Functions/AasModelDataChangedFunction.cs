using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.Functions;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AasFactory
{
    [ExcludeFromCodeCoverage]
    public class AasModelDataChangedFunction : BasicFunction
    {
        public AasModelDataChangedFunction(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        : base(serviceProvider, loggerFactory.CreateLogger<AasModelDataChangedFunction>())
        {
        }

        [FunctionName(nameof(AasModelDataChangedFunction))]
        public void Run(
            [EventHubTrigger("%AAS_EVENT_HUB_NAME%", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData eventData,
            ILogger log)
        {
            this.RunFunction(eventData);
        }
    }
}