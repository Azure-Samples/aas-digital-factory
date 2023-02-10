using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The AASToSubmodel is to define relationship between the AAS shell and sub model twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AASToSubmodel : BasicRelationship
    {
        public AASToSubmodel()
        {
            this.Name = AdtConstants.SubmodelRelationshipName;
        }
    }
}
