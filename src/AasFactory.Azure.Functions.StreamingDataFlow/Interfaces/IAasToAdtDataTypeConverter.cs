using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;

public interface IAasToAdtDataTypeConverter
{
    /// <summary>
    /// Gets the ADT typed value and value key given the property type and property value (as a string).
    /// </summary>
    /// <param name="propertyType">The property type enum value.</param>
    /// <param name="propertyValue">The property value as a string.</param>
    /// <returns>The parse value key and value.</returns>
    KeyValuePair<string, object> ParseValueBasedOnDataTypeAndGetValueKey(PropertyType propertyType, string propertyValue);
}