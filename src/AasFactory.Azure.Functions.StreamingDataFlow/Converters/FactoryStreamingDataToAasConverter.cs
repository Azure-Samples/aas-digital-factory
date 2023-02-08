using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.Logger;
using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Logger;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory.Enums;
using AasFactory.Azure.Models.Factory.StreamingData;
using Microsoft.Extensions.Logging;
using Params = AasFactory.Azure.Functions.StreamingDataFlow.Parameters.FactoryStreamingDataProcessorParameters;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Converters;

public class FactoryStreamingDataToAasConverter : IAdapter<FactoryStreamingDataChanged, AasStreamingDataChanged>
{
    private readonly ILogger<FactoryStreamingDataToAasConverter> logger;

    private readonly IAasIdBuilder aasIdBuilder;

    private readonly IAasDataTypeConverter aasDataTypeConverter;

    private readonly IAdapter<(string, ModelInstanceType), SubModelType> fieldNameToSubmodelTypeConverter;

    private readonly IAdapter<ModelInstanceType, string?> modelInstanceTypeToSourceTimestampFieldNameConverter;

    private readonly IStreamingDataUtils streamingDataUtils;

    public FactoryStreamingDataToAasConverter(
        ILogger<FactoryStreamingDataToAasConverter> logger,
        IAasIdBuilder aasIdBuilder,
        IAasDataTypeConverter aasDataTypeConverter,
        IAdapter<(string, ModelInstanceType), SubModelType> fieldNameToSubmodelTypeConverter,
        IAdapter<ModelInstanceType, string?> modelInstanceTypeToSourceTimestampFieldNameConverter,
        IStreamingDataUtils streamingDataUtils)
    {
        this.logger = logger;
        this.aasIdBuilder = aasIdBuilder;
        this.aasDataTypeConverter = aasDataTypeConverter;
        this.fieldNameToSubmodelTypeConverter = fieldNameToSubmodelTypeConverter;
        this.modelInstanceTypeToSourceTimestampFieldNameConverter = modelInstanceTypeToSourceTimestampFieldNameConverter;
        this.streamingDataUtils = streamingDataUtils;
    }

    /// <inheritdoc />
    public AasStreamingDataChanged Convert(FactoryStreamingDataChanged from)
    {
        var modelInstanceType = from.Header.ModelType;
        var machineId = from.Header.MachineId;

        var sourceTimestampFieldName = this.modelInstanceTypeToSourceTimestampFieldNameConverter.Convert(modelInstanceType);
        if (sourceTimestampFieldName is null)
        {
            this.logger.ModelInstanceTypeDoesNotHaveSourceTimestampFieldConfigured(modelInstanceType);
            return null!;
        }

        var sourceTimestampFieldExistsInFields = from.Data
            .Any(elem => elem.Name == sourceTimestampFieldName);
        if (!sourceTimestampFieldExistsInFields)
        {
            this.logger.FactoryStreamingDataDoesNotContainValidSourceTimestampField(machineId, modelInstanceType, sourceTimestampFieldName);
            return null!;
        }

        var aasStreamingDataChanged = new AasStreamingDataChanged();
        aasStreamingDataChanged.SourceTimestampFieldName = sourceTimestampFieldName;
        aasStreamingDataChanged.Properties = from.Data
            .Where(field => this.IsFieldValid(field, modelInstanceType))
            .Select(field =>
                        new PropertyField
                        {
                            Id = this.aasIdBuilder.BuildSubModelElementId(
                                ModelInstanceType.Machine,
                                machineId,
                                this.fieldNameToSubmodelTypeConverter.Convert((field.Name, modelInstanceType)),
                                field.Id),
                            IdShort = field.Name,
                            Value = field.Value == null! ? null! : JsonWellFormatter<object>.ToString(field.Value),
                            ValueType = this.aasDataTypeConverter.ConvertFactoryDataTypeToAasPropertyType(field.DataType),
                        });

        return aasStreamingDataChanged!;
    }

    private bool IsFieldValid(StreamingDataField field, ModelInstanceType modelInstanceType)
    {
        var isFieldValid = field != default(StreamingDataField) &&
            !string.IsNullOrWhiteSpace(field.Name) &&
            !string.IsNullOrWhiteSpace(field.Id) &&
            this.fieldNameToSubmodelTypeConverter.Convert((field.Name, modelInstanceType)) != SubModelType.Unknown &&
            this.streamingDataUtils.IsValidDataFieldValue(field) &&
            this.aasDataTypeConverter.TryConvertFactoryDataTypeToAasPropertyType(field.DataType, out _);
        return isFieldValid;
    }
}
