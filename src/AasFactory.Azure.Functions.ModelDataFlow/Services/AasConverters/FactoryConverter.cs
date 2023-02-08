using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;
using Params = AasFactory.Azure.Functions.ModelDataFlow.Parameters.AasConversionParameters;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class FactoryConverter : IAdapter<Factory, Shell>
{
    private readonly IAasIdBuilder idBuilder;
    private readonly IConverterHelpers converterHelpers;

    public FactoryConverter(IAasIdBuilder idBuilder, IConverterHelpers converterHelpers)
    {
        this.idBuilder = idBuilder;
        this.converterHelpers = converterHelpers;
    }

    /// <inheritdoc />
    public Shell Convert(Factory factory)
    {
        var id = this.idBuilder.BuildShellId(ModelInstanceType.Factory, factory.Id);
        var machinesSubModelId = this.idBuilder.BuildSubModelId(ModelInstanceType.Factory, factory.Id, SubModelType.Machines);
        var linesSubModelId = this.idBuilder.BuildSubModelId(ModelInstanceType.Factory, factory.Id, SubModelType.Lines);
        var nameplateSubModelId = this.idBuilder.BuildSubModelId(ModelInstanceType.Factory, factory.Id, SubModelType.Nameplate);
        var assetKindValue = AssetKindType.Instance;
        var kindType = KindType.Instance;

        var shell = new Shell()
        {
            Id = id,
            Iri = this.converterHelpers.CreateIri(assetKindValue, id),
            IdShort = factory.Name,
            AssetInformation = new AssetInformation()
            {
                AssetKind = new AssetKind { AssetKindValue = assetKindValue },
            },
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, factory.DisplayName },
                },
            },
            SubModels = new SubModel[]
            {
                new SubModel()
                {
                    Id = nameplateSubModelId,
                    Iri = this.converterHelpers.CreateIri(assetKindValue, nameplateSubModelId, isShell: false),
                    IdShort = SubModelType.Nameplate,
                    Kind = new Kind { KindValue = kindType },
                    DisplayName = new LangStringSet()
                    {
                        LangString = new Dictionary<string, string>()
                        {
                            { Iso2Codes.EnglishCode, SubModelType.Nameplate.ToString() },
                        },
                    },
                    Properties = new Property[]
                    {
                        new Property
                        {
                            Id = this.idBuilder.BuildSubModelElementId(ModelInstanceType.Factory, factory.Id, SubModelType.Nameplate, Params.PropertyPlaceNameIdShort.ToLower()),
                            IdShort = Params.PropertyPlaceNameIdShort,
                            ValueType = PropertyType.String,
                            Kind = new Kind { KindValue = kindType },
                            Value = factory.PlaceName,
                            DisplayName = new LangStringSet()
                            {
                                LangString = new Dictionary<string, string>()
                                {
                                    { Iso2Codes.EnglishCode, Params.PropertyPlaceNameIdShort },
                                },
                            },
                        },
                        new Property
                        {
                            Id = this.idBuilder.BuildSubModelElementId(ModelInstanceType.Factory, factory.Id, SubModelType.Nameplate, Params.PropertyTimeZoneIdShort.ToLower()),
                            IdShort = Params.PropertyTimeZoneIdShort,
                            ValueType = PropertyType.String,
                            Kind = new Kind { KindValue = kindType },
                            Value = factory.TimeZone,
                            DisplayName = new LangStringSet()
                            {
                                LangString = new Dictionary<string, string>()
                                {
                                    { Iso2Codes.EnglishCode, Params.PropertyTimeZoneIdShort },
                                },
                            },
                        },
                    },
                },
                new SubModel()
                {
                    Id = machinesSubModelId,
                    Iri = this.converterHelpers.CreateIri(assetKindValue, machinesSubModelId, isShell: false),
                    IdShort = SubModelType.Machines,
                    Kind = new Kind { KindValue = kindType },
                    ReferenceElements = this.converterHelpers
                        .CreateReferenceElementsFrom(ModelInstanceType.Machine, factory.Machines.Select(ma => new IdentifiableInstance() { Id = ma.Id, Name = ma.Name }), kindType),
                    DisplayName = new LangStringSet()
                    {
                        LangString = new Dictionary<string, string>()
                        {
                            { Iso2Codes.EnglishCode, SubModelType.Machines.ToString() },
                        },
                    },
                },
                new SubModel()
                {
                    Id = linesSubModelId,
                    Iri = this.converterHelpers.CreateIri(assetKindValue, linesSubModelId, isShell: false),
                    IdShort = SubModelType.Lines,
                    Kind = new Kind { KindValue = kindType },
                    ReferenceElements = this.converterHelpers
                        .CreateReferenceElementsFrom(ModelInstanceType.Line, factory.Lines.Select(li => new IdentifiableInstance() { Id = li.Id, Name = li.Name }), kindType),
                    DisplayName = new LangStringSet()
                    {
                        LangString = new Dictionary<string, string>()
                        {
                            { Iso2Codes.EnglishCode, SubModelType.Lines.ToString() },
                        },
                    },
                },
            },
        };

        return shell;
    }
}