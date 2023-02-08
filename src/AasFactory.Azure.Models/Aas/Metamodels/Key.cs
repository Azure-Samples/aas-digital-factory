using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class Key
{
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public KeyType Type { get; set; } = KeyType.None;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}