using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The SubModelElementListToAas is to define relationship between the Sub model element list and AAS twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelElementListToAas : BasicRelationship
    {
        public SubModelElementListToAas()
        {
            this.Name = AdtConstants.ValueRelationshipName;
        }
    }
}
