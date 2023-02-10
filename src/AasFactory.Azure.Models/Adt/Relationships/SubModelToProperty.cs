using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The SubModelToProperty is to define relationship between the sub model and property twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelToProperty : BasicRelationship
    {
        public SubModelToProperty()
        {
            this.Name = AdtConstants.SubmodelElementRelationshipName;
        }
    }
}
