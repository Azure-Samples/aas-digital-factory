using AasFactory.Azure.Models.Aas;
using AasFactory.Azure.Models.Factory;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

public interface IAasShellBuilder
{
    /// <summary>
    /// Facilitates converting model data instances into shells, and building a <see cref="AasShells"/> object from them.
    /// </summary>
    /// <param name="machineTypes">machine types from model data.</param>
    /// <param name="factories">factories from model data.</param>
    /// <param name="lines">lines from model data.</param>
    /// <param name="machines">machines from model data.</param>
    /// <returns>An AasShells object.</returns>
    AasShells BuildAasShells(IEnumerable<MachineType> machineTypes, IEnumerable<Factory> factories, IEnumerable<Line> lines, IEnumerable<Machine> machines);
}