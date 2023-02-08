using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

/// <inheritdoc cref="IAdtRelationshipIdBuilder"/>
public class AdtRelationshipIdBuilder : IAdtRelationshipIdBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdtRelationshipIdBuilder"/> class.
    /// </summary>
    public AdtRelationshipIdBuilder()
    {
    }

    public string BuildRelationshipId(string fromId, string toId) => $"{fromId}_{toId}";
}