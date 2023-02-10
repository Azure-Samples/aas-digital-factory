using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// The Sub Model Element Collection AAS Component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelElementCollection : BasicDigitalTwin
        {
        // <summary>
        /// default constructor
        /// </summary>
        public SubModelElementCollection()
        : base()
        {
        }

        /// <summary>
        /// Constructor to initialise with the AAS input values
        /// </summary>
        /// <param name="smc"></param>
        public SubModelElementCollection(Aas.Metamodels.SubModelElementCollection smc)
        {
            this.Id = smc.Id;
            this.IdShort = smc.IdShort;
            this.Category = smc.Category;
            this.Checksum = smc.Checksum;
            this.SemanticIdValue = smc.SemanticIdValue;
            this.Kind = new Adt.Components.Kind(smc.Kind);
            this.Tags = new Adt.Components.Tags(smc.Tags);
            this.Description = new Adt.Components.LangStringSet(smc.Description);
            this.DisplayName = new Adt.Components.LangStringSet(smc.DisplayName);
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.SubmodelElementCollectionModelId };
        }

        /// <summary>
        /// Gets or sets the id short.
        /// </summary>
        [JsonPropertyName("idShort")]
        public string IdShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        [JsonPropertyName("kind")]
        public Kind Kind { get; set; } = new Kind();

        /// <summary>
        /// Gets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public LangStringSet Description { get; set; } = new LangStringSet();

        /// <summary>
        /// Gets the display name.
        /// </summary>
        [JsonPropertyName("displayName")]
        public LangStringSet DisplayName { get; set; } = new LangStringSet();

        /// <summary>
        /// Gets the tags.
        /// </summary>
        [JsonPropertyName("tags")]
        public Tags Tags { get; set; } = new Tags();

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

        /// <summary>
        /// Gets or sets SemanticIdValue.
        /// </summary>
        [JsonPropertyName("semanticIdValue")]
        public string SemanticIdValue { get; set; } = string.Empty;
        }
}