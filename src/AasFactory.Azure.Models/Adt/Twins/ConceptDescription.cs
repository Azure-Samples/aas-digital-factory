using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    [ExcludeFromCodeCoverage]
    public class ConceptDescription : BasicDigitalTwin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDescription"/> class.
        /// </summary>
        public ConceptDescription()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.ConceptDescriptionModelId };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDescription"/> class.
        /// Converts from an instance of <see cref="Aas.Metamodels.ConceptDescription"/>.
        /// </summary>
        /// <param name="conceptDescription">The AAS Concept Description ontology</param>
        public ConceptDescription(Aas.Metamodels.ConceptDescription conceptDescription)
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.ConceptDescriptionModelId };

            this.Id = conceptDescription.Id;
            this.ID = conceptDescription.Iri;
            this.IdShort = conceptDescription.IdShort;
            this.Administration = new Administration(conceptDescription.Administration);
            this.Description = new LangStringSet(conceptDescription.Description);
            this.DisplayName = new LangStringSet(conceptDescription.DisplayName);
            this.Tags = new Tags(conceptDescription.Tags);
            this.Category = conceptDescription.Category;
            this.Checksum = conceptDescription.Checksum;
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
        ///  Gets or sets the administration.
        /// </summary>
        [JsonPropertyName("administration")]
        public Administration Administration { get; set; } = new ();

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public LangStringSet Description { get; set; } = new ();

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonPropertyName("displayName")]
        public LangStringSet DisplayName { get; set; } = new ();

        /// <summary>
        ///  Gets or sets the tags.
        /// </summary>
        [JsonPropertyName("tags")]
        public Tags Tags { get; set; } = new ();

        /// <summary>
        ///  Gets or sets the category.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the checksum.
        /// </summary>
        [JsonPropertyName("checksum")]
        public string Checksum { get; set; } = string.Empty;
    }
}