using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The Value is to define relationship between the Sub model and AAS twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SubModelToElementCollection : BasicRelationship
    {
        public SubModelToElementCollection()
        {
            this.Name = AdtConstants.SubmodelElementRelationshipName;
        }
    }
}