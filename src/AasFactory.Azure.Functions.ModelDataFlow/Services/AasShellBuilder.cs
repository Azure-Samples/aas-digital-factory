using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Models.Aas;
using AasFactory.Azure.Models.Factory;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

public class AasShellBuilder : IAasShellBuilder
{
    private readonly IAasConverter aasConverter;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AasShellBuilder"/> class.
    /// </summary>
    /// <param name="aasConverter">The aas converter.</param>
    public AasShellBuilder(IAasConverter aasConverter, ILogger<AasShellBuilder> logger)
    {
        this.aasConverter = aasConverter;
        this.logger = logger;
    }

    /// <inheritdoc />
    public AasShells BuildAasShells(IEnumerable<MachineType> machineTypes, IEnumerable<Factory> factories, IEnumerable<Line> lines, IEnumerable<Machine> machines)
    {
        this.logger.BuildingAasShells();

        var machineTypeDictionary = machineTypes.ToDictionary(machineType => machineType.Id);

        var factoryShells = this.aasConverter.ConvertFactories(factories);
        var lineShells = this.aasConverter.ConvertLines(lines);
        var machineShells = this.aasConverter.ConvertMachines(machines, machineTypeDictionary);
        var machineTypeShells = this.aasConverter.ConvertMachineTypes(machineTypes); // machine types should contain at least 1 azure event hub for each item in the list.
        var conceptDescriptions = this.aasConverter.GetConceptDescriptions(machineTypes);

        var shells = new AasShells
        {
            Factories = factoryShells.ToArray(),
            MachineTypes = machineTypeShells.ToArray(),
            Lines = lineShells.ToArray(),
            Machines = machineShells.ToArray(),
            ConceptDescriptions = conceptDescriptions.ToArray(),
        };

        return shells;
    }
}
