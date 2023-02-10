using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Lang String Set AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class LangStringSet
{
    /// <summary>
    /// Gets the Lang String Set.
    /// </summary>
    public IDictionary<string, string> LangString { get; set; } = new Dictionary<string, string>();
}
