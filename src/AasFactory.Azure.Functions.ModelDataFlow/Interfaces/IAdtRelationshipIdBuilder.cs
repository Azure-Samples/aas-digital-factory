using System;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

/// <summary>
/// Builds Ids for Adt relationship classes.
/// </summary>
public interface IAdtRelationshipIdBuilder
{
    /// <summary>
    /// Build Id for Adt relationship class.
    /// </summary>
    /// <param name="fromId">Id of from object.</param>
    /// <param name="toId">Id of to object.</param>
    /// <returns>Id of relationship connecting <paramref name="fromId"/> to <paramref name="toId"/></returns>
    string BuildRelationshipId(string fromId, string toId);
}