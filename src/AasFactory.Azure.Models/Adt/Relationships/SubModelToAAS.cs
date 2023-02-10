using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The SubModelToAAS is to define relationship between the Sub model and AAS twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelToAAS : BasicRelationship
    {
        public SubModelToAAS()
        {
            this.Name = AdtConstants.ReferredElementRelationshipName;
        }
    }
}
