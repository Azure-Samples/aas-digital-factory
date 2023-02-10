using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Tags AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class Tags
{
    /// <summary>
    /// Gets the Markers.
    /// </summary>
    public IDictionary<string, string> Markers { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets the Values.
    /// </summary>
    public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
}
