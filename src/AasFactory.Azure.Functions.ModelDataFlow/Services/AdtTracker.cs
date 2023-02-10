using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

[ExcludeFromCodeCoverage]

/// <inheritdoc/>
public class AdtTracker : IAdtTracker
{
    private ISet<string> adtTwinIds;

    private ISet<string> adtRelationIds;

    public AdtTracker()
    {
        this.adtTwinIds = new HashSet<string>();
        this.adtRelationIds = new HashSet<string>();
    }

    public void AddTwinId(string twinId)
    {
        this.adtTwinIds.Add(twinId);
    }

    public IEnumerable<string> GetTwinIds()
    {
        return this.adtTwinIds;
    }

    public void ClearTwinIds()
    {
        this.adtTwinIds = new HashSet<string>();
    }

    public void AddRelationshipId(string relationshipId)
    {
        this.adtRelationIds.Add(relationshipId);
    }

    public IEnumerable<string> GetRelationshipIds()
    {
        return this.adtRelationIds;
    }

    public void ClearRelationshipIds()
    {
        this.adtRelationIds = new HashSet<string>();
    }
}
