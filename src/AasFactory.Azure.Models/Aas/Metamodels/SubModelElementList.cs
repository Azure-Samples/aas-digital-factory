using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Sub Model Element List AAS Component.
/// </summary>
[ExcludeFromCodeCoverage]
public class SubModelElementList
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
    /// Gets or sets the kind.
    /// </summary>
    public Kind Kind { get; set; } = new ();

    /// <summary>
    /// Gets the description.
    /// </summary>
    public LangStringSet Description { get; set; } = new ();

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public LangStringSet DisplayName { get; set; } = new ();

    /// <summary>
    /// Gets the tags.
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
    /// Gets or sets the boolean representing if order is relevant.
    /// </summary>
    public bool OrderRelevant { get; set; } = false;

    /// <summary>
    /// Gets or sets SemanticIdValue.
    /// </summary>
    public string SemanticIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets SemanticIdValueOfListElements.
    /// </summary>
    public string SemanticIdValueOfListElements { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets TypeValueListElement.
    /// </summary>
    public TypeValueListElement TypeValueListElement { get; set; } = TypeValueListElement.None;

    /// <summary>
    /// Gets or sets ValueTypeOfElement.
    /// </summary>
    public PropertyType ValueTypeOfElement { get; set; } = PropertyType.None;

    /// <summary>
    /// Gets or sets the list of reference elements.
    /// </summary>
    public IEnumerable<ReferenceElement> ReferenceElements { get; set; } = Enumerable.Empty<ReferenceElement>();

    /// <summary>
    /// Gets or sets the list of properties.
    /// </summary>
    public IEnumerable<Property> Properties { get; set; } = Enumerable.Empty<Property>();
}
