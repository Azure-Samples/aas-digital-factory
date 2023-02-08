using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class Property : PropertyField
{
    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public LangStringSet Description { get; set; } = new ();

    /// <summary>
    /// Gets or sets Display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets or sets Tags.
    /// </summary>
    public Tags Tags { get; set; } = new ();

    /// <summary>
    /// Gets or sets Kind.
    /// </summary>
    public Kind Kind { get; set; } = new ();

    /// <summary>
    /// Gets or sets Category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checksum.
    /// </summary>
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets Semantic Id Value.
    /// </summary>
    public string SemanticIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the semantic id reference.
    /// </summary>
    public Reference? SemanticIdReference { get; set; }
}