using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
     /// <summary>
    /// The Administration AAS component to interface with Adt.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Administration : BasicDigitalTwinComponent
    {
        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        public Administration()
        {
        }

        /// <summary>
        /// Constructor to initialise properties
        /// </summary>
        /// <param name="admin"></param>
        public Administration(Aas.Metamodels.Administration admin)
        {
            this.Revision = admin.Revision;
            this.Version = admin.Version;
        }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public new BasicComponentMetadata Metadata { get; set; } = new BasicComponentMetadata();

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Revision.
        /// </summary>
        [JsonPropertyName("revision")]
        public string Revision { get; set; } = string.Empty;
    }
}