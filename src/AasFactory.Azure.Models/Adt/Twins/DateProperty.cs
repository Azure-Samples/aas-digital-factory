using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// DateProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DateProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public DateProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DatePropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public DateProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidDate = DateOnly.TryParse(property.Value, out _);
                if (!isValidDate)
                {
                    throw new Exception("The value of the property is not a valid date.");
                }

                this.Contents.Add(AdtConstants.DatePropertyKey, property.Value);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DatePropertyModelId };
        }
    }
}