using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The Kind AAS component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Kind : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public Kind()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="kind"></param>
        public Kind(Aas.Metamodels.Kind kind)
        {
            this.KindValue = kind.KindValue.ToString();
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        [JsonPropertyName("kind")]
        public string KindValue { get; set; } = string.Empty;
    }
}
