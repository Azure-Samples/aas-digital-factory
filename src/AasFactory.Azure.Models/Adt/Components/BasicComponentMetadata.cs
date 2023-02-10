using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Components
{
    /// <summary>
    /// The Basic component's metadata
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BasicComponentMetadata
    {
        /// <summary>
        /// Gets or sets the component metadata.
        /// </summary>
        public DigitalTwinPropertyMetadata ComponentMetadata { get; set; } = new DigitalTwinPropertyMetadata();
    }
}