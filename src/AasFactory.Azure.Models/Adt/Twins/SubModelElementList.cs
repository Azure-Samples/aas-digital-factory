using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// The Sub Model Element List AAS Component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelElementList : BasicDigitalTwin
    {
        // <summary>
        /// default constructor
        /// </summary>
        public SubModelElementList()
        : base()
        {
        }

        /// <summary>
        /// Constructor to initialise with the AAS input values
        /// </summary>
        /// <param name="sml"></param>
        public SubModelElementList(Aas.Metamodels.SubModelElementList sml)
        {
            this.Category = sml.Category;
            this.Checksum = sml.Checksum;
            this.Description = new Adt.Components.LangStringSet(sml.Description);
            this.DisplayName = new Adt.Components.LangStringSet(sml.DisplayName);
            this.Id = sml.Id;
            this.IdShort = sml.IdShort;
            this.Kind = new Adt.Components.Kind(sml.Kind);
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.SubmodelElementListModelId };
            this.OrderRelevant = sml.OrderRelevant;
            this.SemanticIdValue = sml.SemanticIdValue;
            this.SemanticIdValueOfListElements = sml.SemanticIdValueOfListElements;
            this.TypeValueListElement = sml.TypeValueListElement.ToString();
            this.Tags = new Adt.Components.Tags(sml.Tags);
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
        /// Gets or sets the boolean representing if order is relevant.
        /// </summary>
        [JsonPropertyName("orderRelevant")]
        public bool OrderRelevant { get; set; } = false;

        /// <summary>
        /// Gets or sets SemanticIdValue.
        /// </summary>
        [JsonPropertyName("semanticIdValue")]
        public string SemanticIdValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets SemanticIdValueOfListElements.
        /// </summary>
        [JsonPropertyName("semanticIdListElementValue")]
        public string SemanticIdValueOfListElements { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets TypeValueListElement.
        /// </summary>
        [JsonPropertyName("valueTypeListElement")]
        public string TypeValueListElement { get; set; } = string.Empty;
    }
}
