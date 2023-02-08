using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// FloatProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FloatProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public FloatProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.FloatPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public FloatProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidFloat = float.TryParse(property.Value, out var floatValue);
                if (!isValidFloat)
                {
                    throw new Exception("The value of the property is not a valid float.");
                }

                this.Contents.Add(AdtConstants.FloatPropertyKey, floatValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.FloatPropertyModelId };
        }
    }
}