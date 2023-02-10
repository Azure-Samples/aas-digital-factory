using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships;

[ExcludeFromCodeCoverage]
public class PropertyToReference : BasicRelationship
{
    public PropertyToReference()
    {
        this.Name = AdtConstants.SemanticIdRelationshipName;
    }
}