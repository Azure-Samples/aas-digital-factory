using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Factory.Enums;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.Logger;

[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 200,
        Level = LogLevel.Error,
        EventName = "FailedToProcessFunction",
        Message = "Failed to process a function")]
    public static partial void FailedToProcessFunction(this ILogger logger, Exception ex);

    [LoggerMessageAttribute(
        EventId = 201,
        Level = LogLevel.Debug,
        EventName = "UnableToConvertFactoryFieldTypeToAasPropertyType",
        Message = "Unable to convert Factory field type {dataType} to Aas PropertyType")]
    public static partial void UnableToConvertFactoryFieldTypeToAasPropertyType(this ILogger logger, DataType dataType);
}
