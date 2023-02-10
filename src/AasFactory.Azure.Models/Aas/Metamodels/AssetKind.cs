using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class AssetKind
{
    /// <summary>
    /// Gets or sets the AssetKindValue.
    /// </summary>
    [JsonProperty("assetKind")]
    public AssetKindType AssetKindValue { get; set; } = AssetKindType.None;
}
