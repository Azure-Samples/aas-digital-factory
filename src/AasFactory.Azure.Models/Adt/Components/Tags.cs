using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The Tags AAS component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Tags : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public Tags()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="tags"></param>
        public Tags(Aas.Metamodels.Tags tags)
        {
            this.Markers = tags.Markers;
            this.Values = tags.Values;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Gets or sets the markers.
        /// </summary>
        [JsonPropertyName("markers")]
        public IDictionary<string, string> Markers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        [JsonPropertyName("values")]
        public IDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}
