using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships;

[ExcludeFromCodeCoverage]
public class ReferenceToConceptDescription : BasicRelationship
{
    public ReferenceToConceptDescription()
    {
        this.Name = AdtConstants.ReferredElementRelationshipName;
    }
}