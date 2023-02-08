using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The SubModelElementCollectionToElementList is to define relationship between the Sub model element collection and Sub model element list twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelElementCollectionToElementList : BasicRelationship
    {
        public SubModelElementCollectionToElementList()
        {
            this.Name = AdtConstants.ValueRelationshipName;
        }
    }
}