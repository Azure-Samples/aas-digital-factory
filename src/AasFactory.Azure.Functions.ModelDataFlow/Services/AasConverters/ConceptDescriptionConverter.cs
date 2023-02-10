using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;
using Params = AasFactory.Azure.Functions.ModelDataFlow.Parameters.AasConversionParameters;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;

public class ConceptDescriptionConverter : IAdapter<(MachineTypeField field, string machineTypeId, SubModelType subModelType), ConceptDescription>
{
    private readonly IAasIdBuilder idBuilder;
    private readonly IConverterHelpers converterHelpers;
    private readonly IAasDataTypeConverter dataTypeConverter;

    public ConceptDescriptionConverter(IAasIdBuilder idBuilder, IConverterHelpers converterHelpers, IAasDataTypeConverter dataTypeConverter)
    {
        this.idBuilder = idBuilder;
        this.converterHelpers = converterHelpers;
        this.dataTypeConverter = dataTypeConverter;
    }

    /// <inheritdoc />
    public ConceptDescription Convert((MachineTypeField field, string machineTypeId, SubModelType subModelType) converterValues)
    {
        var id = this.idBuilder.BuildConceptDescriptionId(ModelInstanceType.MachineType, converterValues.machineTypeId, converterValues.subModelType, converterValues.field.Id);
        var conceptDescription = new ConceptDescription()
        {
            Id = id,
            IdShort = Params.ConceptDescriptionIdShort,
            ReferenceElementIds = new string[] { this.idBuilder.BuildReferenceId(id) },
            DisplayName = new LangStringSet()
            {
                LangString = new Dictionary<string, string>()
                {
                    { Iso2Codes.EnglishCode, Params.ConceptDescriptionIdShort },
                },
            },
            DataSpecification = new DataSpecification()
            {
                Id = this.idBuilder.BuildDataSpecificationId(ModelInstanceType.MachineType, converterValues.machineTypeId, converterValues.subModelType, converterValues.field.Id),
                IdShort = Params.DataSpecificationIdShort,
                DataType = this.dataTypeConverter.ConvertFactoryDataTypeToAasDataType(converterValues.field.DataType),
                LevelType = this.dataTypeConverter.ConvertFactoryStatTypeToAasLevelType(converterValues.field.StatType),
                Unit = converterValues.field.Unit,
            },
        };

        return conceptDescription;
    }
}