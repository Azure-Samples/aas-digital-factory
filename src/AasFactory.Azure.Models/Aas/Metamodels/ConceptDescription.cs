using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class ConceptDescription
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the iri.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id short.
    /// </summary>
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of reference element ids.
    /// </summary>
    public IEnumerable<string> ReferenceElementIds { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// Gets or sets the administration.
    /// </summary>
    public Administration Administration { get; set; } = new ();

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public LangStringSet Description { get; set; } = new ();

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    public Tags Tags { get; set; } = new ();

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checksum.
    /// </summary>
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data specification.
    /// </summary>
    public DataSpecification DataSpecification { get; set; } = new ();
}