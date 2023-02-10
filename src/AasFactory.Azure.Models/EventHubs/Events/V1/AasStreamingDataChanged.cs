using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Models.EventHubs.Events.V1;

/// <summary>
/// Azure integration event whenever Aas streaming data changed.
/// </summary>
public class AasStreamingDataChanged : IntegrationEvent
{
    /// <summary>
    /// Gets or sets the source timestamp field name.
    /// </summary>
    public string SourceTimestampFieldName { get; set; } = string.Empty;

    /// <summary>
    /// The list of all fields associated with a streaming data cycle
    /// </summary>
    /// <typeparam name="PropertyField"></typeparam>
    /// <returns></returns>
    public IEnumerable<PropertyField> Properties { get; set; } = Enumerable.Empty<PropertyField>();
}
