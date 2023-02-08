using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// Property AAS twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Property : BasicDigitalTwin
    {
        /// <summary>
        /// Constructor to assign AAS input values
        /// </summary>
        /// <param name="property"></param>
        public Property(Aas.Metamodels.Property property)
        {
            this.Category = property.Category;
            this.Checksum = property.Checksum;
            this.Description = new Adt.Components.LangStringSet(property.Description);
            this.DisplayName = new Adt.Components.LangStringSet(property.DisplayName);
            this.Id = property.Id;
            this.IdShort = property.IdShort;
            this.SemanticIdValue = property.SemanticIdValue;
            this.Kind = new Adt.Components.Kind(property.Kind);
            this.Tags = new Adt.Components.Tags(property.Tags);
            this.Value = property.Value;
            this.ValueType = property.ValueType.ToString();
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Property()
        {
        }

        /// <summary>
        ///  Gets or sets the value type
        /// </summary>
        [JsonPropertyName("valueType")]
        public string ValueType { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the value
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the semantic id value
        /// </summary>
        [JsonPropertyName("semanticIdValue")]
        public string SemanticIdValue { get; set; } = string.Empty;

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
        ///  Gets or sets the category
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the check sum
        /// </summary>
        [JsonPropertyName("checksum")]
        public string Checksum { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the id short
        /// </summary>
        [JsonPropertyName("idShort")]
        public string IdShort { get; set; } = string.Empty;
    }
}