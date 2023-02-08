using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Asset Information AAS component.
/// </summary>
[ExcludeFromCodeCoverage]
public class AssetInformation
{
    /// <summary>
    /// Gets or sets the AssetKind.
    /// </summary>
    public AssetKind AssetKind { get; set; } = new ();

    /// <summary>
    /// Gets or sets the DefaultThumbnailPath.
    /// </summary>
    public string DefaultThumbnailPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the GlobalAssetIdValue.
    /// </summary>
    public string GlobalAssetIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SpecificAssetIdValue.
    /// </summary>
    public string SpecificAssetIdValue { get; set; } = string.Empty;
}