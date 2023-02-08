using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Shell AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class Shell
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the IRI.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Id short.
    /// </summary>
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type the class is derived from.
    /// </summary>
    public string DerivedFrom { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of reference element ids.
    /// </summary>
    public IEnumerable<string> ReferenceElementIds { get; set; } = Enumerable.Empty<string>();

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
    /// Gets the administration (versioned component).
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
    /// Gets or sets the sub models.
    /// </summary>
    public IEnumerable<SubModel> SubModels { get; set; } = Enumerable.Empty<SubModel>();

    /// <summary>
    /// Gets or sets the asset information.
    /// </summary>
    public AssetInformation AssetInformation { get; set; } = new ();
}