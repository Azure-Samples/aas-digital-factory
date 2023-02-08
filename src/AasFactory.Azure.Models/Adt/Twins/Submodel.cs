using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// Submodel AAS twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Submodel : BasicDigitalTwin
    {
        /// <summary>
        /// Sub model constructor to initialise with the AAS input values
        /// </summary>
        public Submodel()
        {
            this.Description = new Adt.Components.LangStringSet();
            this.DisplayName = new Adt.Components.LangStringSet();
            this.Kind = new Adt.Components.Kind();
            this.Tags = new Adt.Components.Tags();
            this.Administration = new Adt.Components.Administration();
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.SubmodelModelId };
        }

        /// <summary>
        /// Sub model constructor to initialise with the AAS input values
        /// </summary>
        /// <param name="sub"></param>
        public Submodel(Aas.Metamodels.SubModel sub)
        {
            this.Category = sub.Category;
            this.Checksum = sub.Checksum;
            this.ID = sub.Iri;
            this.Id = sub.Id;
            this.IdShort = sub.IdShort.ToString();
            this.SemanticIdValue = sub.SemanticIdValue;
            this.Description = new Adt.Components.LangStringSet(sub.Description);
            this.DisplayName = new Adt.Components.LangStringSet(sub.DisplayName);
            this.Kind = new Adt.Components.Kind(sub.Kind);
            this.Tags = new Adt.Components.Tags(sub.Tags);
            this.Administration = new Adt.Components.Administration(sub.Administration);
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.SubmodelModelId };
        }

        /// <summary>
        /// Gets or sets the IRI value.
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the semantic id value
        /// </summary>
        [JsonPropertyName("semanticIdValue")]
        public string SemanticIdValue { get; set; } = string.Empty;

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
        ///  Gets or sets the kind
        /// </summary>
        [JsonPropertyName("kind")]
        public Kind Kind { get; set; } = new Kind();

        /// <summary>
        ///  Gets or sets the administration
        /// </summary>
        [JsonPropertyName("administration")]
        public Administration Administration { get; set; } = new Administration();

        /// <summary>
        /// Gets or sets the Id short.
        /// </summary>
        [JsonPropertyName("idShort")]
        public string IdShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        [JsonPropertyName("checksum")]
        public string Checksum { get; set; } = string.Empty;
    }
}