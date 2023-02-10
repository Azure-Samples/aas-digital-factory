using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// DateTimeProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DateTimeProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public DateTimeProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DateTimePropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public DateTimeProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidDateTime = DateTime.TryParse(property.Value, out var dateTimeValue);
                if (!isValidDateTime)
                {
                    throw new Exception("The value of the property is not a valid date time.");
                }

                this.Contents.Add(AdtConstants.DateTimePropertyKey, dateTimeValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DateTimePropertyModelId };
        }
    }
}