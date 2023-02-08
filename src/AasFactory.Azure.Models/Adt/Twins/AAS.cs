using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// Asset Administration shell AAS twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AAS : BasicDigitalTwin
    {
        /// <summary>
        /// Constructor to initialize AAS twin
        /// </summary>
        public AAS()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.AASModelId };
        }

        /// <summary>
        /// Constructor to initialize AAS twin
        /// </summary>
        /// <param name="shell">The AAS Shell</param>
        public AAS(Aas.Metamodels.Shell shell)
        {
            this.Id = shell.Id;
            this.IdShort = shell.IdShort;
            this.ID = shell.Iri;
            this.Category = shell.Category;
            this.Checksum = shell.Checksum;
            this.Administration = new Adt.Components.Administration(shell.Administration);
            this.Tags = new Adt.Components.Tags(shell.Tags);
            this.Description = new LangStringSet(shell.Description);
            this.DisplayName = new LangStringSet(shell.DisplayName);
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.AASModelId };
            this.AssetInformation = new Adt.Components.AssetInformation(shell.AssetInformation);
        }

        /// <summary>
        /// Gets or sets the IRI value.
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Id short.
        /// </summary>
        [JsonPropertyName("idShort")]
        public string IdShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public LangStringSet Description { get; set; } = new LangStringSet();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonPropertyName("displayName")]
        public LangStringSet DisplayName { get; set; } = new LangStringSet();

        /// <summary>
        ///  Gets or sets the tags
        /// </summary>
        [JsonPropertyName("tags")]
        public Tags Tags { get; set; } = new Tags();

        /// <summary>
        ///  Gets or sets the administration
        /// </summary>
        [JsonPropertyName("administration")]
        public Administration Administration { get; set; } = new Administration();

        /// <summary>
        ///  Gets or sets the category
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the checksum
        /// </summary>
        [JsonPropertyName("checksum")]
        public string Checksum { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the asset informationConstructor to assign AAS input values
        /// </summary>
        [JsonPropertyName("assetInformationShort")]
        public AssetInformation AssetInformation { get; set; } = new AssetInformation();
    }
}