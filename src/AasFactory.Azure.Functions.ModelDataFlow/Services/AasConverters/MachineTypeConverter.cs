using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class MachineTypeConverter : IAdapter<MachineType, Shell>
{
    private readonly IAasIdBuilder idBuilder;
    private readonly IConverterHelpers converterHelpers;

    public MachineTypeConverter(IAasIdBuilder idBuilder, IConverterHelpers converterHelpers)
    {
        this.idBuilder = idBuilder;
        this.converterHelpers = converterHelpers;
    }

    /// <inheritdoc />
    public Shell Convert(MachineType machineType)
    {
        // since we ensure all machine templates have the "machine_type" model instance we can use first.
        var machineTypeId = machineType.Id;
        var name = machineType.Name;
        var displayName = machineType.DisplayName;
        var assetKindType = AssetKindType.Type;
        var kindType = KindType.Template;

        var fieldAndType = this.converterHelpers.OrganizeMachineTypeFields(machineType);
        var subModels = this.converterHelpers.BuildSubModels(fieldAndType, ModelInstanceType.MachineType, machineTypeId, kindType, assetKindType);

        var shellId = this.idBuilder.BuildShellId(ModelInstanceType.MachineType, machineTypeId);
        var shell = new Shell()
        {
            Id = shellId,
            Iri = this.converterHelpers.CreateIri(assetKindType, shellId),
            IdShort = name,
            AssetInformation = new AssetInformation()
            {
                AssetKind = new AssetKind { AssetKindValue = assetKindType },
            },
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, displayName },
                },
            },
            SubModels = subModels,
        };

        return shell;
    }
}