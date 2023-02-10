namespace AasFactory.Azure.Models.EventHubs;

public class MetaData
{
    /// <summary>
    /// Gets or sets the partition key of the message.
    /// </summary>
    public string PartitionKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the telemetry trace id.
    /// </summary>
    public string TraceId { get; set; } = string.Empty;

    /// <summary>
    /// time that event has been pushed to eventhubs
    /// </summary>
    public DateTimeOffset QueuedTime { get; set; } = DateTimeOffset.UnixEpoch;
}
