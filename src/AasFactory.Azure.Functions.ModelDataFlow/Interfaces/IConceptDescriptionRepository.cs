using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

/// <summary>
/// Adt Repository for <see cref="ConceptDescription"/>, handles <see cref="DataSpecification"/> as well.
/// </summary>
public interface IConceptDescriptionRepository
{
    /// <summary>
    /// Creates or replaces the AAS Concept Description into the data store.
    /// </summary>
    /// <param name="conceptDescription">The AAS Concept Description ontology.</param>
    /// <returns></returns>
    void CreateOrReplaceConceptDescription(ConceptDescription conceptDescription);
}
