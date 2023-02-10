using Aas = AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces
{
    /// <summary>
    /// Repository to handle ADT graph operations
    /// </summary>
    public interface IGraphRepository : IBaseAdtRepository
    {
        /// <summary>
        /// Remove the twins and relationships that are no longer needed
        /// </summary>
        /// <returns></returns>
        void RemoveUnwantedGraph();

        /// <summary>
        /// Create or replace twins and relationships for model data flow
        /// </summary>
        /// <param name="shells"></param>
        /// <returns></returns>
        void CreateOrReplaceGraph(IEnumerable<Aas.Shell> shells, IEnumerable<Aas.ConceptDescription> conceptDescriptions);
    }
}