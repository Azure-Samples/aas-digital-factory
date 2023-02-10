using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class MachineConverter : IAdapter<(Machine machine, MachineType machineType), Shell>
{
    private readonly IAasIdBuilder idBuilder;
    private readonly IConverterHelpers converterHelpers;

    public MachineConverter(IAasIdBuilder idBuilder, IConverterHelpers converterHelpers)
    {
        this.idBuilder = idBuilder;
        this.converterHelpers = converterHelpers;
    }

    /// <inheritdoc />
    public Shell Convert((Machine machine, MachineType machineType) machineAndMachineType)
    {
        var assetKindType = AssetKindType.Instance;
        var kindType = KindType.Instance;

        var machineTypeId = machineAndMachineType.machineType.Id;
        var fieldAndType = this.converterHelpers.OrganizeMachineTypeFields(machineAndMachineType.machineType);
        var subModels = this.converterHelpers.BuildSubModels(fieldAndType, ModelInstanceType.Machine, machineAndMachineType.machine.Id, kindType, assetKindType);

        var referenceId = this.idBuilder.BuildReferenceElementId(ModelInstanceType.Machine, machineAndMachineType.machine.Id);

        var id = this.idBuilder.BuildShellId(ModelInstanceType.Machine, machineAndMachineType.machine.Id);
        var shell = new Shell()
        {
            Id = id,
            Iri = this.converterHelpers.CreateIri(assetKindType, id),
            IdShort = machineAndMachineType.machine.Name,
            ReferenceElementIds = new List<string>() { referenceId },
            DerivedFrom = this.idBuilder.BuildShellId(ModelInstanceType.MachineType, machineTypeId),
            AssetInformation = new AssetInformation()
            {
                AssetKind = new AssetKind { AssetKindValue = assetKindType },
            },
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, machineAndMachineType.machine.DisplayName },
                },
            },
            SubModels = subModels,
        };

        return shell;
    }
}