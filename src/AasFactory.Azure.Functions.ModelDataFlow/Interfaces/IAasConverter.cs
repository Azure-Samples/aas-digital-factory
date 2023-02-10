namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Factory;

/// <summary>
/// Converts the raw data to the corresponding AAS representation.
/// </summary>
public interface IAasConverter
{
    /// <summary>
    /// Converts a collection of Factories to a collection of AAS shells.
    /// </summary>
    /// <param name="factories">ienumerable of factory.</param>
    /// <returns>shells</returns>
    public IEnumerable<Shell> ConvertFactories(IEnumerable<Factory> factories);

    /// <summary>
    /// Converts a collection of Machine Types to a collection of AAS shells.
    /// </summary>
    /// <param name="machineTypes">ienuerable of machine types.</param>
    /// <returns>shells.</returns>
    public IEnumerable<Shell> ConvertMachineTypes(IEnumerable<MachineType> machineTypeGroups);

    /// <summary>
    /// Converts a collection of types to a collection of AAS shells.
    /// </summary>
    /// <param name="lines">ienumerable line types.</param>
    /// <returns>shells.</returns>
    public IEnumerable<Shell> ConvertLines(IEnumerable<Line> lines);

    /// <summary>
    /// Converts a collection of Machines to a collection of AAS shells.
    /// </summary>
    /// <param name="machines">ienumerable of machines.</param>
    /// <param name="machineTypeMap">map from machine type name to machine type.</param>
    /// <returns>shells.</returns>
    public IEnumerable<Shell> ConvertMachines(IEnumerable<Machine> machines, IDictionary<string, MachineType> machineTypeMap);

    /// <summary>
    /// Generates a collection of Concept Descriptions based on Machine Types.
    /// </summary>
    /// <param name="machineTypes">list of machine types.</param>
    /// <returns>concept descriptions.</returns>
    public IEnumerable<ConceptDescription> GetConceptDescriptions(IEnumerable<MachineType> machineTypes);
}
