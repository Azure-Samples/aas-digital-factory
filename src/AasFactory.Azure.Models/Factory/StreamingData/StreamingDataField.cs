using Newtonsoft.Json;
using AasFactoryEnums = AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Models.Factory.StreamingData;

/// <summary>
/// Any machine instance field with a value
/// </summary>
public class StreamingDataField
{
    // <summary>
    /// Gets or sets the id.
    /// </summary>
    [JsonRequired]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data type.
    /// </summary>
    [JsonRequired]
    public AasFactoryEnums.DataType DataType { get; set; } = AasFactoryEnums.DataType.Unknown;

    /// <summary>
    /// Generally all values will be deserialized as string regardless of their types.
    /// </summary>
    /// <value></value>
    public object? Value { get; set; } = null;
}
