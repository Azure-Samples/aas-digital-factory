using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Administration AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class Administration
{
    /// <summary>
    /// Gets or sets the Version.
    /// </summary>
    public string Version { get; set; } = "1";

    /// <summary>
    /// Gets or sets the Revision.
    /// </summary>
    public string Revision { get; set; } = "1";
}
