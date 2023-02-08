using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    /// <summary>
    /// StringProperty twin definition
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StringProperty : Property
    {
        // <summary>
        /// default constructor
        /// </summary>
        public StringProperty()
        : base()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.StringPropertyModelId };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public StringProperty(Aas.Metamodels.Property property)
        : base(property)
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.StringPropertyModelId };
        }
    }
}