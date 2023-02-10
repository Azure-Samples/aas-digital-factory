using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Reference Element AAS Component.
/// </summary>
[ExcludeFromCodeCoverage]
public class ReferenceElement
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Id Short.
    /// </summary>
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the semantic id value.
    /// </summary>
    public string SemanticIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets key 1.
    /// </summary>
    public Key Key1 => new ();

    /// <summary>
    /// Gets key 2.
    /// </summary>
    public Key Key2 => new ();

    /// <summary>
    /// Gets key 3.
    /// </summary>
    public Key Key3 => new ();

    /// <summary>
    /// Gets key 4.
    /// </summary>
    public Key Key4 => new ();

    /// <summary>
    /// Gets key 5.
    /// </summary>
    public Key Key5 => new ();

    /// <summary>
    /// Gets key 6.
    /// </summary>
    public Key Key6 => new ();

    /// <summary>
    /// Gets key 7.
    /// </summary>
    public Key Key7 => new ();

    /// <summary>
    /// Gets key 8.
    /// </summary>
    public Key Key8 => new ();

    /// <summary>
    /// Gets the description.
    /// </summary>
    public LangStringSet Description => new ();

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    public Tags Tags { get; set; } = new ();

    /// <summary>
    /// Gets or sets the kind.
    /// </summary>
    public Kind Kind { get; set; } = new ();

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checksum.
    /// </summary>
    public string Checksum { get; set; } = string.Empty;
}
