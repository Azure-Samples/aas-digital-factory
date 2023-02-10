using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Sub Model Element Collection AAS Component.
/// </summary>
[ExcludeFromCodeCoverage]
public class SubModelElementCollection
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id short.
    /// </summary>
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the submodel element.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checksum of the colletion.
    /// </summary>
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the semantic id of the colletion.
    /// </summary>
    public string SemanticIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the kind.
    /// </summary>
    public Kind Kind { get; set; } = new ();

    /// <summary>
    /// Gets the tags.
    /// </summary>
    public Tags Tags { get; set; } = new ();

    /// <summary>
    /// Gets the description.
    /// </summary>
    public LangStringSet Description { get; set; } = new ();

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets or sets the list of submodel element collections.
    /// </summary>
    public IEnumerable<SubModelElementCollection> SubModelElementCollections { get; set; } = Enumerable.Empty<SubModelElementCollection>();

    /// <summary>
    /// Gets or sets the list of reference elements.
    /// </summary>
    public IEnumerable<ReferenceElement> ReferenceElements { get; set; } = Enumerable.Empty<ReferenceElement>();

    /// <summary>
    /// Gets or sets the list of submodel element lists.
    /// </summary>
    public IEnumerable<SubModelElementList> SubModelElementLists { get; set; } = Enumerable.Empty<SubModelElementList>();
}