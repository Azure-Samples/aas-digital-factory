using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The Lang String Set AAS component.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LangStringSet : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public LangStringSet()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="lang"></param>
        public LangStringSet(Aas.Metamodels.LangStringSet lang)
        {
            this.LangString = lang.LangString;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Gets or sets the lang string.
        /// </summary>
        [JsonPropertyName("langString")]
        public IDictionary<string, string> LangString { get; set; } = new Dictionary<string, string>();
    }
}