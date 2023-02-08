using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// Reference Element AAS twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ReferenceElement : BasicDigitalTwin
    {
        // <summary>
        /// default constructor
        /// </summary>
        public ReferenceElement()
        {
        }

        /// <summary>
        /// Constructor to intialise with the AAS input values
        /// </summary>
        /// <param name="refe"></param>
        public ReferenceElement(Aas.Metamodels.ReferenceElement refe)
        {
            this.Id = refe.Id;
            this.Key1 = new BasicDigitalTwinComponent();
            this.Key2 = new BasicDigitalTwinComponent();
            this.Key3 = new BasicDigitalTwinComponent();
            this.Key4 = new BasicDigitalTwinComponent();
            this.Key5 = new BasicDigitalTwinComponent();
            this.Key6 = new BasicDigitalTwinComponent();
            this.Key7 = new BasicDigitalTwinComponent();
            this.Key8 = new BasicDigitalTwinComponent();
            this.Category = refe.Category;
            this.Checksum = refe.Checksum;
            this.Description = new Adt.Components.LangStringSet(refe.Description);
            this.DisplayName = new Adt.Components.LangStringSet(refe.DisplayName);
            this.IdShort = refe.IdShort;
            this.Tags = new Adt.Components.Tags(refe.Tags);
            this.Kind = new Adt.Components.Kind(refe.Kind);
            this.SemanticIdValue = refe.SemanticIdValue;
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.ReferenceElementModelId };
        }

        /// <summary>
        /// Gets or sets key 1.
        /// </summary>
        [JsonPropertyName("key1")]
        public BasicDigitalTwinComponent Key1 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 2.
        /// </summary>
        [JsonPropertyName("key2")]
        public BasicDigitalTwinComponent Key2 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 3.
        /// </summary>
        [JsonPropertyName("key3")]
        public BasicDigitalTwinComponent Key3 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 4.
        /// </summary>
        [JsonPropertyName("key4")]
        public BasicDigitalTwinComponent Key4 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 5.
        /// </summary>
        [JsonPropertyName("key5")]
        public BasicDigitalTwinComponent Key5 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 6.
        /// </summary>
        [JsonPropertyName("key6")]
        public BasicDigitalTwinComponent Key6 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 7.
        /// </summary>
        [JsonPropertyName("key7")]
        public BasicDigitalTwinComponent Key7 { get; set; } = new BasicDigitalTwinComponent();

        /// <summary>
        /// Gets or sets key 8.
        /// </summary>
        [JsonPropertyName("key8")]
        public BasicDigitalTwinComponent Key8 { get; set; } = new BasicDigitalTwinComponent();

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
        /// Gets or sets the Id Short.
        /// </summary>
        [JsonPropertyName("idShort")]
        public string IdShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the semantic id value.
        /// </summary>
        [JsonPropertyName("semanticIdValue")]
        public string SemanticIdValue { get; set; } = string.Empty;

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
