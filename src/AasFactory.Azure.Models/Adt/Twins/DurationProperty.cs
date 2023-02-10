using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// DurationProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DurationProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public DurationProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DurationPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public DurationProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidTimeSpan = TimeSpan.TryParse(property.Value, out var timeSpanValue);
                if (!isValidTimeSpan)
                {
                    throw new Exception("The value of the property is not a valid time span.");
                }

                this.Contents.Add(AdtConstants.DurationPropertyKey, timeSpanValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DurationPropertyModelId };
        }
    }
}