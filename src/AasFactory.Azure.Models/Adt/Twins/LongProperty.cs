using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// LongProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LongProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public LongProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.LongPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public LongProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            if (!string.IsNullOrEmpty(property.Value))
            {
                var isValidLong = long.TryParse(property.Value, out var longValue);
                if (!isValidLong)
                {
                    throw new Exception("The value of the property is not a valid long.");
                }

                this.Contents.Add(AdtConstants.LongPropertyKey, longValue);
            }

            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.LongPropertyModelId };
        }
    }
}