using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The Key AAS component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Key : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public Key()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="key"></param>
        public Key(Aas.Metamodels.Key key)
        {
            this.Type = key.Type.ToString();
            this.Value = key.Value;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Key's type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Key's value
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
    }
}
