using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// TimeProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TimeProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public TimeProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.TimePropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public TimeProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidTime = TimeOnly.TryParse(property.Value, out _);
                if (!isValidTime)
                {
                    throw new Exception("The value of the property is not a valid time.");
                }

                this.Contents.Add(AdtConstants.TimePropertyKey, property.Value);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.TimePropertyModelId };
        }
    }
}