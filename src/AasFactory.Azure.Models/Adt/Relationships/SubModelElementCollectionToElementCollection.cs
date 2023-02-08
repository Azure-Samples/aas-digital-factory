using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The SubModelElementCollectionToElementCollection is to define relationship between the two Sub model element collection twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelElementCollectionToElementCollection : BasicRelationship
    {
        public SubModelElementCollectionToElementCollection()
        {
            this.Name = AdtConstants.ValueRelationshipName;
        }
    }
}