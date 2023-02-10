using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Models.Adt;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory.Enums;
using AasFactory.Azure.Models.Factory.StreamingData;
using AasEnum = AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Utils;

public class StreamingDataUtils : IStreamingDataUtils
{
    /// <inheritdoc />
    public StreamingDataField? ExtractDataField(FactoryStreamingDataChanged streamingData, string propertyName)
    {
        var propertyData = streamingData.Data
            .FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.InvariantCultureIgnoreCase));
        return propertyData;
    }

    /// <inheritdoc />
    public bool IsValidDataFieldValue(StreamingDataField? dataField)
    {
        if (dataField is null)
        {
            return false;
        }

        var valueStringified = dataField.Value?.ToString();
        if (valueStringified is null)
        {
            return false;
        }

        return dataField.DataType switch
        {
            DataType.Int => int.TryParse(valueStringified, out _),
            DataType.BigInt => long.TryParse(valueStringified, out _),
            DataType.Float32 => float.TryParse(valueStringified, out _),
            DataType.Float64 => double.TryParse(valueStringified, out _),
            DataType.Boolean => bool.TryParse(valueStringified, out _),
            DataType.DateTime => DateTime.TryParse(valueStringified, out _),
            DataType.String => true,
            var unknown => false,
        };
    }

    public string GetAdtModelIdForPropertyValueType(AasEnum.PropertyType type)
    {
        return type switch
        {
            AasEnum.PropertyType.Boolean => AdtConstants.BooleanPropertyModelId,
            AasEnum.PropertyType.Date => AdtConstants.DatePropertyModelId,
            AasEnum.PropertyType.DateTime => AdtConstants.DateTimePropertyModelId,
            AasEnum.PropertyType.Double => AdtConstants.DoublePropertyModelId,
            AasEnum.PropertyType.Duration => AdtConstants.DurationPropertyModelId,
            AasEnum.PropertyType.Float => AdtConstants.FloatPropertyModelId,
            AasEnum.PropertyType.Integer => AdtConstants.IntegerPropertyModelId,
            AasEnum.PropertyType.Long => AdtConstants.LongPropertyModelId,
            AasEnum.PropertyType.String => AdtConstants.StringPropertyModelId,
            AasEnum.PropertyType.Time => AdtConstants.TimePropertyModelId,
            var none => string.Empty
        };
    }

    public bool IsStreamingDataPayloadValid(FactoryStreamingDataChanged from)
    {
        return from != null &&
               from.Header != null && !string.IsNullOrEmpty(from.Header.MachineId) &&
               from.Data != null && from.Data?.Count() > 0;
    }
}