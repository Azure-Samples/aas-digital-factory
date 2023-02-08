using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// BooleanProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BooleanProperty : Property
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public BooleanProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.BooleanPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public BooleanProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidBoolean = bool.TryParse(property.Value, out var booleanValue);
                if (!isValidBoolean)
                {
                    throw new Exception("The value of the property is not a valid boolean.");
                }

                this.Contents.Add(AdtConstants.BooleanPropertyKey, booleanValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.BooleanPropertyModelId };
        }
    }
}