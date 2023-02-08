using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// IntegerProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class IntegerProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public IntegerProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.IntegerPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public IntegerProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidInteger = int.TryParse(property.Value, out var integerValue);
                if (!isValidInteger)
                {
                    throw new Exception("The value of the property is not a valid integer.");
                }

                this.Contents.Add(AdtConstants.IntPropertyKey, integerValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.IntegerPropertyModelId };
        }
    }
}