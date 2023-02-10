namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

using System.Collections.Generic;
using System.Linq;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;
using Microsoft.Extensions.Logging;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Functions.Interfaces;

/// <inheritdoc cref="IAasConverter"/>
public class AasConverter : IAasConverter
{
    private readonly IConverterHelpers converterHelpers;
    private readonly IAdapter<Line, Shell> lineConverter;
    private readonly IAdapter<Factory, Shell> factoryConverter;
    private readonly IAdapter<MachineType, Shell> machineTypeConverter;
    private readonly IAdapter<(Machine machine, MachineType machineType), Shell> machineConverter;
    private readonly IAdapter<(MachineTypeField field, string machineTypeId, SubModelType subModelType), ConceptDescription> conceptDescriptionConverter;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SmToAasConverter"/> class.
    /// </summary>
    /// <param name="idBuilder">id builder.</param>
    /// <param name="modelDataFlowSettings">settings / config.</param>
    /// <param name="dataTypeConverter">data type converter.</param>
    public AasConverter(
        IConverterHelpers converterHelpers,
        IAdapter<Line, Shell> lineConverter,
        IAdapter<Factory, Shell> factoryConverter,
        IAdapter<MachineType, Shell> machineTypeConverter,
        IAdapter<(Machine machine, MachineType machineType), Shell> machineConverter,
        IAdapter<(MachineTypeField field, string machineTypeId, SubModelType subModelType), ConceptDescription> conceptDescriptionConverter,
        ILogger<AasConverter> logger)
    {
        this.converterHelpers = converterHelpers;
        this.lineConverter = lineConverter;
        this.factoryConverter = factoryConverter;
        this.machineTypeConverter = machineTypeConverter;
        this.machineConverter = machineConverter;
        this.conceptDescriptionConverter = conceptDescriptionConverter;
        this.logger = logger;
    }

    public IEnumerable<Shell> ConvertFactories(IEnumerable<Factory> factories) =>
        factories.Select(fa =>
        {
            this.logger.ConvertingFactoryData(fa.Id, fa.Name);
            return this.factoryConverter.Convert(fa);
        });

    public IEnumerable<Shell> ConvertMachineTypes(IEnumerable<MachineType> machineTypes) =>
        machineTypes.Select(machineType =>
        {
            this.logger.ConvertingMachineTypeData(machineType.Id, machineType.Name);
            return this.machineTypeConverter.Convert(machineType);
        });

    public IEnumerable<Shell> ConvertLines(IEnumerable<Line> lines) =>
        lines.Select(li =>
        {
            this.logger.ConvertingLineData(li.Id, li.Name);
            return this.lineConverter.Convert(li);
        });

    public IEnumerable<Shell> ConvertMachines(IEnumerable<Machine> machines, IDictionary<string, MachineType> machineTypeMap) =>
        machines.Select(ma =>
        {
            this.logger.ConvertingMachineData(ma.Id, ma.Name);
            return this.machineConverter.Convert((ma, machineTypeMap[ma.MachineType.Id]));
        });

    public IEnumerable<ConceptDescription> GetConceptDescriptions(IEnumerable<MachineType> machineTypes)
    {
        foreach (var machineType in machineTypes)
        {
            var organizedFields = this.converterHelpers.OrganizeMachineTypeFields(machineType);
            foreach (var (type, listOfFields) in organizedFields)
            {
                foreach (var field in listOfFields)
                {
                    if (!this.converterHelpers.FieldCanCreateConceptDescription(field))
                    {
                        continue;
                    }

                    this.logger.ConvertingConceptDescription(field.Id, field.Name, machineType.Id, machineType.Name);
                    yield return this.conceptDescriptionConverter.Convert((field, machineType.Id, type));
                }
            }
        }
    }
}
