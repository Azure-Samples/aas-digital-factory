using AasFactory.Azure.Models.Factory.StreamingData;

namespace AasFactory.Azure.Models.EventHubs.Events.V1;

/// <summary>
/// Azure integration event for any streaming data
/// </summary>
public class FactoryStreamingDataChanged : IntegrationEvent
{
    /// <summary>
    /// Header for streaming data containing machine instance information
    /// </summary>
    /// <value></value>
    public StreamingDataHeader Header { get; set; } = new ();

    /// <summary>
    /// a list of fields of streaming data
    /// </summary>
    /// <typeparam name="StreamingDataField"></typeparam>
    /// <returns></returns>
    public IEnumerable<StreamingDataField> Data { get; set; } = Enumerable.Empty<StreamingDataField>();
}
