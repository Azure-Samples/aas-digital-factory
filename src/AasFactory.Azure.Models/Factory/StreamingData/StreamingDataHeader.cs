using AasFactory.Azure.Models.Factory.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AasFactory.Azure.Models.Factory.StreamingData;

/// <summary>
/// The headers in the streaming data payloads.
/// </summary>
public class StreamingDataHeader
{
    /// <summary>
    /// Gets or sets the machine id.
    /// </summary>
    [JsonRequired]
    public string MachineId { get; set; } = string.Empty;

    /// <summary>
    /// Model type that would be machine type in case of cycle streaming data
    /// </summary>
    /// <value></value>
    [JsonRequired]
    [JsonConverter(typeof(StringEnumConverter))]
    public ModelInstanceType ModelType { get; set; } = ModelInstanceType.Unknown;
}