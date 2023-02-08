using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;
using Params = AasFactory.Azure.Functions.ModelDataFlow.Parameters.AasConversionParameters;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class ConverterHelpers : IConverterHelpers
{
    private const string AasOntologyAbbreviation = "aas";
    private const string SubModelOntologyAbbreviation = "sm";

    private readonly IAasIdBuilder idBuilder;
    private readonly IModelDataFlowSettings modelDataFlowSettings;
    private readonly IAasDataTypeConverter dataTypeConverter;

    public ConverterHelpers(IAasIdBuilder idBuilder, IModelDataFlowSettings modelDataFlowSettings, IAasDataTypeConverter dataTypeConverter)
    {
        this.idBuilder = idBuilder;
        this.modelDataFlowSettings = modelDataFlowSettings;
        this.dataTypeConverter = dataTypeConverter;
    }

    /// <inheritdoc />
    public string CreateIri(AssetKindType assetKind, string id, bool isShell = true)
    {
        var aasOntologyAbbreviated = isShell ? AasOntologyAbbreviation : SubModelOntologyAbbreviation;
        var url = $"{Params.IriBaseUrl}/{this.modelDataFlowSettings.AbbreviatedCompanyName}/{aasOntologyAbbreviated}/{assetKind.ToString()}/{id}".ToLower();
        return url;
    }

    /// <inheritdoc />
    public IEnumerable<ReferenceElement> CreateReferenceElementsFrom(
        ModelInstanceType toType,
        IEnumerable<IdentifiableInstance> identifiableInstances,
        KindType kind)
    {
        return identifiableInstances.Select(identifiableInstance =>
        {
            var idShort = $"{identifiableInstance.Name}Ref";
            return new ReferenceElement()
            {
                Id = this.idBuilder.BuildReferenceElementId(toType, identifiableInstance.Id),
                IdShort = idShort,
                Kind = new Kind { KindValue = kind },
                DisplayName = new LangStringSet()
                {
                    LangString = new Dictionary<string, string>()
                    {
                        { Iso2Codes.EnglishCode, idShort },
                    },
                },
            };
        });
    }

    /// <inheritdoc />
    public bool FieldCanCreateConceptDescription(MachineTypeField field) =>
        field.StatType is not StatType.None || !string.IsNullOrEmpty(field.Unit);

    /// <inheritdoc />
    public IDictionary<SubModelType, IList<MachineTypeField>> OrganizeMachineTypeFields(MachineType machineType)
    {
        var modelInstanceType = ModelInstanceType.MachineType;
        var machineTypeFields = new Dictionary<SubModelType, IList<MachineTypeField>>();
        foreach (var field in machineType.Fields)
        {
            // if the mapping does not exist for a model data sub model we exit early
            if (!AasMappingConfig.Mapping.TryGetValue(modelInstanceType, out var subModelMappings))
            {
                throw new Exception($"Model type {modelInstanceType} is not configured in the mapping!");
            }

            // loop through the sub model mappings to see if mapped
            var found = false;
            foreach (var subModelMapping in subModelMappings.SubModels)
            {
                found = subModelMapping.SubModelElements.Any(elem => string.Equals(elem.Name, field.Name, StringComparison.InvariantCultureIgnoreCase));
                if (found)
                {
                    this.InsertOrAppendField(machineTypeFields, subModelMapping.SubModel, field);
                    break;
                }
            }

            // if not mapped place in default sub model.
            if (!found)
            {
                this.InsertOrAppendField(machineTypeFields, subModelMappings.DefaultSubModel, field);
            }
        }

        return machineTypeFields;
    }

    /// <inheritdoc />
    public IEnumerable<SubModel> BuildSubModels(
        IDictionary<SubModelType, IList<MachineTypeField>> fieldsDictionary,
        ModelInstanceType modelInstanceType,
        string modelInstanceId,
        KindType kindType,
        AssetKindType assetKindType)
    {
        var subModels = fieldsDictionary
            .Select(fields =>
            {
                var id = this.idBuilder.BuildSubModelId(modelInstanceType, modelInstanceId, fields.Key);
                var properties = fields.Value.Select(field =>
                    this.BuildPropertyFromMachineTypeField(modelInstanceId, modelInstanceType, field, kindType, fields.Key));
                return new SubModel()
                {
                    Id = id,
                    Iri = this.CreateIri(assetKindType, id, isShell: false),
                    IdShort = fields.Key,
                    Kind = new Kind { KindValue = kindType },
                    Properties = properties,
                    DisplayName = new LangStringSet()
                    {
                        LangString = new Dictionary<string, string>()
                        {
                            { Iso2Codes.EnglishCode, fields.Key.ToString() },
                        },
                    },
                };
            });

        return subModels;
    }

    private void InsertOrAppendField(
        IDictionary<SubModelType, IList<MachineTypeField>> fieldsDictionary,
        SubModelType key,
        MachineTypeField value)
    {
        if (fieldsDictionary.TryGetValue(key, out _))
        {
            fieldsDictionary[key].Add(value);
            return;
        }

        fieldsDictionary[key] = new List<MachineTypeField>() { value };
    }

    private Property BuildPropertyFromMachineTypeField(
        string machineTypeId,
        ModelInstanceType modelInstanceType,
        MachineTypeField field,
        KindType kindType,
        SubModelType subModelType)
    {
        var id = this.idBuilder.BuildSubModelElementId(modelInstanceType, machineTypeId, subModelType, field.Id);
        Reference? reference = null;
        string semanticId = string.Empty;

        if (this.FieldCanCreateConceptDescription(field) && modelInstanceType == ModelInstanceType.MachineType)
        {
            semanticId = this.idBuilder.BuildConceptDescriptionId(ModelInstanceType.MachineType, machineTypeId, subModelType, field.Id);
            reference = new Reference()
            {
                Id = this.idBuilder.BuildReferenceId(semanticId),
                Type = ReferenceType.ModelReference,
            };
        }

        return new Property()
        {
            Id = id,
            IdShort = field.Name,
            ValueType = this.dataTypeConverter.ConvertFactoryDataTypeToAasPropertyType(field.DataType),
            Kind = new Kind { KindValue = kindType },
            SemanticIdValue = semanticId,
            SemanticIdReference = reference,
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, field.DisplayName },
                },
            },
        };
    }
}