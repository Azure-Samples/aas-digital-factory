using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class LineConverter : IAdapter<Line, Shell>
{
    private readonly IAasIdBuilder idBuilder;
    private readonly IConverterHelpers converterHelpers;

    public LineConverter(IAasIdBuilder idBuilder, IConverterHelpers converterHelpers)
    {
        this.idBuilder = idBuilder;
        this.converterHelpers = converterHelpers;
    }

    /// <inheritdoc />
    public Shell Convert(Line line)
    {
        var id = this.idBuilder.BuildShellId(ModelInstanceType.Line, line.Id);
        var machinesSubModelId = this.idBuilder.BuildSubModelId(ModelInstanceType.Line, line.Id, SubModelType.Machines);
        var processFlowSubModelId = this.idBuilder.BuildSubModelId(ModelInstanceType.Line, line.Id, SubModelType.ProcessFlow);
        var lineReferenceElementId = this.idBuilder.BuildReferenceElementId(ModelInstanceType.Line, line.Id);

        var assetKindType = AssetKindType.Instance;
        var kindType = KindType.Instance;

        var successorName = "Successors";
        var predecessorName = "Predecessors";

        // Line instance
        var shell = new Shell()
        {
            Id = id,
            Iri = this.converterHelpers.CreateIri(assetKindType, id),
            IdShort = line.Name,
            ReferenceElementIds = new string[]
            {
                lineReferenceElementId,
            },
            AssetInformation = new AssetInformation()
            {
                AssetKind = new AssetKind { AssetKindValue = assetKindType },
            },
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, line.DisplayName },
                },
            },
            SubModels = new SubModel[]
            {
                new SubModel()
                {
                    Id = machinesSubModelId,
                    Iri = this.converterHelpers.CreateIri(assetKindType, machinesSubModelId, isShell: false),
                    IdShort = SubModelType.Machines,
                    Kind = new Kind { KindValue = kindType },
                    ReferenceElements = this.converterHelpers
                        .CreateReferenceElementsFrom(ModelInstanceType.Machine, line.Topology.Select(tp => new IdentifiableInstance() { Id = tp.Id, Name = tp.Name }), kindType),
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
                    Id = processFlowSubModelId,
                    Iri = this.converterHelpers.CreateIri(assetKindType, processFlowSubModelId, isShell: false),
                    IdShort = SubModelType.ProcessFlow,
                    Kind = new Kind { KindValue = kindType },
                    DisplayName = new LangStringSet()
                    {
                        LangString = new Dictionary<string, string>()
                        {
                            { Iso2Codes.EnglishCode, SubModelType.ProcessFlow.ToString() },
                        },
                    },

                    // Machines
                    SubModelElementCollections = new SubModelElementCollection[]
                    {
                        new SubModelElementCollection()
                        {
                            Id = this.idBuilder.BuildSubModelElementCollectionId(ModelInstanceType.Line, line.Id, SubModelType.ProcessFlow),
                            IdShort = SubModelType.Machines.ToString(),
                            Kind = new Kind { KindValue = kindType },
                            DisplayName = new LangStringSet()
                            {
                                LangString = new Dictionary<string, string>()
                                {
                                    { Iso2Codes.EnglishCode, SubModelType.Machines.ToString() },
                                },
                            },

                            // Machine Names
                            SubModelElementCollections = line.Topology.Select(tp => new SubModelElementCollection()
                            {
                                Id = this.idBuilder.BuildSubModelElementCollectionFieldId(ModelInstanceType.Line, line.Id, SubModelType.ProcessFlow, tp.Id),
                                IdShort = tp.Name,
                                Kind = new Kind { KindValue = kindType },
                                DisplayName = new LangStringSet()
                                {
                                    LangString = new Dictionary<string, string>()
                                    {
                                        { Iso2Codes.EnglishCode, tp.Name },
                                    },
                                },
                                ReferenceElements =
                                    this.converterHelpers.CreateReferenceElementsFrom(
                                            ModelInstanceType.Machine, new IdentifiableInstance[] { new IdentifiableInstance() { Id = tp.Id, Name = tp.Name }, }, kindType),
                                SubModelElementLists = new SubModelElementList[]
                                {
                                    // Successor
                                    new SubModelElementList()
                                    {
                                        Id = this.idBuilder.BuildSubModelElementListId(ModelInstanceType.Line, line.Id, SubModelType.ProcessFlow, tp.Id, "s"),
                                        IdShort = successorName,
                                        Kind = new Kind { KindValue = kindType },
                                        DisplayName = new LangStringSet()
                                        {
                                            LangString = new Dictionary<string, string>()
                                            {
                                                { Iso2Codes.EnglishCode, successorName },
                                            },
                                        },
                                        TypeValueListElement = TypeValueListElement.ReferenceElement,
                                        ReferenceElements = tp.Successors.Select(
                                            succ => this.converterHelpers.CreateReferenceElementsFrom(
                                                ModelInstanceType.Machine, new IdentifiableInstance[] { new IdentifiableInstance() { Id = succ.Id, Name = succ.Name }, }, kindType).ElementAt(0)),
                                    },

                                    // Predecessor
                                    new SubModelElementList()
                                    {
                                        Id = this.idBuilder.BuildSubModelElementListId(ModelInstanceType.Line, line.Id, SubModelType.ProcessFlow, tp.Id, "p"),
                                        IdShort = predecessorName,
                                        Kind = new Kind { KindValue = kindType },
                                        DisplayName = new LangStringSet()
                                        {
                                            LangString = new Dictionary<string, string>()
                                            {
                                                { Iso2Codes.EnglishCode, predecessorName },
                                            },
                                        },
                                        TypeValueListElement = TypeValueListElement.ReferenceElement,
                                        ReferenceElements = tp.Predecessors.Select(
                                            pred => this.converterHelpers.CreateReferenceElementsFrom(
                                                ModelInstanceType.Machine, new IdentifiableInstance[] { new IdentifiableInstance() { Id = pred.Id, Name = pred.Name }, }, kindType).ElementAt(0)),
                                    },
                                },
                            }),
                        },
                    },
                },
            },
        };

        return shell;
    }
}