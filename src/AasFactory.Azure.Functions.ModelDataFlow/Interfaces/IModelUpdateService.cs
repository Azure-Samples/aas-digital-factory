using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces
{
    public interface IModelUpdateService
    {
        /// <summary>
        /// Persist AAS Shells (as Factories, Machines, MachineTypes, Lines) and AAS Concept Descriptions
        /// as an entire connected graph into the storage layer.
        /// </summary>
        /// <param name="shells">A collection of AAS Shell ontologies</param>
        /// <param name="conceptDescriptions">A collection of AAS Concept Description ontologies</param>
        /// <returns></returns>
        void BuildGraph(IEnumerable<Shell> shells, IEnumerable<ConceptDescription> conceptDescriptions);
    }
}