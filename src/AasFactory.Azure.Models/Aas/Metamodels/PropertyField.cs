using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// All specific fields of an AAS property
/// </summary>
[ExcludeFromCodeCoverage]
public class PropertyField
{
    /// <summary>
    /// Gets or sets Id.
    /// </summary>
    [JsonRequired]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets Id Short.
    /// </summary>
    [JsonRequired]
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets Value Type.
    /// </summary>
    [JsonRequired]
    public PropertyType ValueType { get; set; } = PropertyType.None;

    /// <summary>
    /// Gets or sets Value.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
