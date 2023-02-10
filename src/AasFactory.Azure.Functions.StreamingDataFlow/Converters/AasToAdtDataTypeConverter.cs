using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Adt;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Converters;

public class AasToAdtDataTypeConverter : IAasToAdtDataTypeConverter
{
    /// <inheritdoc />
    public KeyValuePair<string, object> ParseValueBasedOnDataTypeAndGetValueKey(PropertyType propertyType, string propertyValue)
    {
        return propertyType switch
        {
            PropertyType.Boolean => new KeyValuePair<string, object>(AdtConstants.BooleanPropertyKey, bool.Parse(propertyValue)),
            PropertyType.Date => new KeyValuePair<string, object>(AdtConstants.DatePropertyKey, DateOnly.Parse(propertyValue)),
            PropertyType.DateTime => new KeyValuePair<string, object>(AdtConstants.DateTimePropertyKey, DateTime.Parse(propertyValue)),
            PropertyType.Double => new KeyValuePair<string, object>(AdtConstants.DoublePropertyKey, double.Parse(propertyValue)),
            PropertyType.Duration => new KeyValuePair<string, object>(AdtConstants.DurationPropertyKey, TimeSpan.Parse(propertyValue)),
            PropertyType.Float => new KeyValuePair<string, object>(AdtConstants.FloatPropertyKey, float.Parse(propertyValue)),
            PropertyType.Integer => new KeyValuePair<string, object>(AdtConstants.IntPropertyKey, int.Parse(propertyValue)),
            PropertyType.Long => new KeyValuePair<string, object>(AdtConstants.LongPropertyKey, long.Parse(propertyValue)),
            PropertyType.String => new KeyValuePair<string, object>(AdtConstants.StringPropertyKey, propertyValue),
            PropertyType.Time => new KeyValuePair<string, object>(AdtConstants.TimePropertyKey, TimeOnly.Parse(propertyValue)),
            _ => throw new ArgumentException($"The property type {propertyType.ToString()} does not have a conversion."),
        };
    }
}