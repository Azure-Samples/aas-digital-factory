using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The AssetKind AAS component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssetKind : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public AssetKind()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="assetKind"></param>
        public AssetKind(Aas.Metamodels.AssetKind assetKind)
        {
            this.Kind = assetKind.AssetKindValue.ToString();
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Gets or sets the asset kind.
        /// </summary>
        [JsonPropertyName("assetKind")]
        public string Kind { get; set; } = string.Empty;
    }
}