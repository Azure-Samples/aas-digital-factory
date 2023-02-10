using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// Asset Information AAS twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssetInformation : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to assign AAS input values
        /// </summary>
        public AssetInformation()
        {
        }

        /// <summary>
        /// Constructor to assign AAS input values
        /// </summary>
        /// <param name="assetInfo"></param>
        public AssetInformation(Aas.Metamodels.AssetInformation assetInfo)
        {
            this.AssetKind = assetInfo.AssetKind.AssetKindValue.ToString();
            this.GlobalAssetIdValue = assetInfo.GlobalAssetIdValue;
            this.SpecificAssetIdValues = assetInfo.SpecificAssetIdValue;
            this.DefaultThumbnailpath = assetInfo.DefaultThumbnailPath;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        ///  Gets or sets the asset kind
        /// </summary>
        [JsonPropertyName("assetKind")]
        public string AssetKind { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the global asset id value
        /// </summary>
        [JsonPropertyName("globalAssetId")]
        public string GlobalAssetIdValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the apecific asset id values
        /// </summary>
        [JsonPropertyName("specificAssetId")]
        public string SpecificAssetIdValues { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the apecific asset id values
        /// </summary>
        [JsonPropertyName("defaultThumbnailpath")]
        public string DefaultThumbnailpath { get; set; } = string.Empty;
    }
}