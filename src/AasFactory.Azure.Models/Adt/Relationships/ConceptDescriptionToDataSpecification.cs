using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships;

[ExcludeFromCodeCoverage]
public class ConceptDescriptionToDataSpecification : BasicRelationship
{
    public ConceptDescriptionToDataSpecification()
    {
        this.Name = AdtConstants.DataSpecificationRelationshipName;
    }
}