using System.Text;
using Azure.Messaging.EventHubs;
using Newtonsoft.Json;

namespace AasFactory.Azure.Models.EventHubs.Extensions;

public static class EventDataExtensions
{
    public static IntegrationEvent? TryGetEvent(this EventData eventData)
    {
        var eventTypeName = GetEventType(eventData);
        var integrationEventType = typeof(IntegrationEvent);

        if (!eventTypeName.StartsWith("AasFactory.Azure.Models.EventHubs.Events"))
        {
            return null;
        }

        var eventType = Type.GetType($"{eventTypeName}, AasFactory.Azure.Models");

        if (eventType == null)
        {
            return null;
        }

        var jsonBody = Encoding.UTF8.GetString(eventData.EventBody);
        var eventTypeValue = JsonConvert.DeserializeObject(jsonBody, eventType);

        if (eventTypeValue == null)
        {
            return null;
        }

        var integrationEvent = (IntegrationEvent)eventTypeValue;
        integrationEvent.MetaData.PartitionKey = eventData.PartitionKey;
        integrationEvent.MetaData.QueuedTime = eventData.EnqueuedTime;

        integrationEvent.MetaData.TraceId =
            string.IsNullOrWhiteSpace(eventData.CorrelationId) ? Guid.NewGuid().ToString() : eventData.CorrelationId;

        return integrationEvent;
    }

    public static string GetEventType(this EventData eventData)
    {
        return (string)eventData.Properties["EventType"];
    }

    public static void SetEventType(this EventData eventData, string eventType)
    {
        eventData.Properties["EventType"] = eventType;
    }
}
