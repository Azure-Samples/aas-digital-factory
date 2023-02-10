using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// The Reference AAS Component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Reference : BasicDigitalTwin
    {
        /// <summary>
        /// Constructor to initialize AAS Reference twin
        /// </summary>
        public Reference()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.ReferenceModelId };
        }

        /// <summary>
        /// Constructor to initialize AAS Reference twin
        /// </summary>
        /// <param name="refe">The AAS Reference ontology</param>
        public Reference(Aas.Metamodels.Reference refe)
        {
            this.Id = refe.Id;
            this.Type = refe.Type.ToString();
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.ReferenceModelId };
        }

        /// <summary>
        /// Gets key 1.
        /// </summary>
        [JsonPropertyName("key1")]
        public BasicDigitalTwinComponent Key1 { get; set; } = new ();

        /// <summary>
        /// Gets key 2.
        /// </summary>
        [JsonPropertyName("key2")]
        public BasicDigitalTwinComponent Key2 { get; set; } = new ();

        /// <summary>
        /// Gets key 3.
        /// </summary>
        [JsonPropertyName("key3")]
        public BasicDigitalTwinComponent Key3 { get; set; } = new ();

        /// <summary>
        /// Gets key 4.
        /// </summary>
        [JsonPropertyName("key4")]
        public BasicDigitalTwinComponent Key4 { get; set; } = new ();

        /// <summary>
        /// Gets key 5.
        /// </summary>
        [JsonPropertyName("key5")]
        public BasicDigitalTwinComponent Key5 { get; set; } = new ();

        /// <summary>
        /// Gets key 6.
        /// </summary>
        [JsonPropertyName("key6")]
        public BasicDigitalTwinComponent Key6 { get; set; } = new ();

        /// <summary>
        /// Gets key 7.
        /// </summary>
        [JsonPropertyName("key7")]
        public BasicDigitalTwinComponent Key7 { get; set; } = new ();

        /// <summary>
        /// Gets key 8.
        /// </summary>
        [JsonPropertyName("key8")]
        public BasicDigitalTwinComponent Key8 { get; set; } = new ();

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}