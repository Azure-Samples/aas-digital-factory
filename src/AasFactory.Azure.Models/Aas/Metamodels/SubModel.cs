using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Sub Model AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class SubModel
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Iri.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Id short.
    /// </summary>
    public SubModelType IdShort { get; set; } = SubModelType.Unknown;

    /// <summary>
    /// Gets or sets the Kind.
    /// </summary>
    public Kind Kind { get; set; } = new ();

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public LangStringSet Description { get; set; } = new ();

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets the tags.
    /// </summary>
    public Tags Tags { get; set; } = new ();

    /// <summary>
    /// Gets the administration.
    /// </summary>
    public Administration Administration { get; set; } = new ();

    /// <summary>
    /// Gets or sets the Category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checksum.
    /// </summary>
    public string Checksum { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SemanticIdValue.
    /// </summary>
    public string SemanticIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets Properties.
    /// </summary>
    public IEnumerable<Property> Properties { get; set; } = Enumerable.Empty<Property>();

    /// <summary>
    /// Gets or sets the list of reference elements.
    /// </summary>
    public IEnumerable<ReferenceElement> ReferenceElements { get; set; } = Enumerable.Empty<ReferenceElement>();

    /// <summary>
    /// Gets or sets the the list of sub model element lists.
    /// </summary>
    public IEnumerable<SubModelElementList> SubModelElementLists { get; set; } = Enumerable.Empty<SubModelElementList>();

    /// <summary>
    /// Gets or sets the the list of sub model element collections.
    /// </summary>
    public IEnumerable<SubModelElementCollection> SubModelElementCollections { get; set; } = Enumerable.Empty<SubModelElementCollection>();
}