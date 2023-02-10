using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// DoubleProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DoubleProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public DoubleProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DoublePropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public DoubleProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidDouble = double.TryParse(property.Value, out var doubleValue);
                if (!isValidDouble)
                {
                    throw new Exception("The value of the property is not a valid double.");
                }

                this.Contents.Add(AdtConstants.DoublePropertyKey, doubleValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DoublePropertyModelId };
        }
    }
}