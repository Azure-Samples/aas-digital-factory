using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Factory.Enums;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Logger;

[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessageAttribute(
    EventId = 2000,
    Level = LogLevel.Warning,
    EventName = "FactoryStreamingInvalid",
    Message = "There is no data field or missing model type information in factory streaming data payload {streamingDataId}")]
    public static partial void FactoryStreamingInvalid(this ILogger logger, string streamingDataId);

    [LoggerMessageAttribute(
    EventId = 2001,
    Level = LogLevel.Warning,
    EventName = "FactoryStreamingDataIncomplete",
    Message = "There is no machine field in factory streaming data payload {streamingDataId}.")]
    public static partial void FactoryStreamingDataIncomplete(this ILogger logger, string streamingDataId);

    [LoggerMessage(
    EventId = 2002,
    Level = LogLevel.Information,
    EventName = "ProcessedAasToAdtStreamingData",
    Message = "AAS property updates to ADT flow ran successfully.")]
    public static partial void ProcessedAasToAdtStreamingData(this ILogger logger);

    [LoggerMessage(
    EventId = 2003,
    Level = LogLevel.Error,
    EventName = "PropertyUpdateFailedInvalidPropertyType",
    Message = "Failed to update a property - {propertyId} because the property value type {propertyType} is not allowed.")]
    public static partial void PropertyUpdateFailedInvalidPropertyType(this ILogger logger, string propertyId, string propertyType);

    [LoggerMessage(
    EventId = 2004,
    Level = LogLevel.Debug,
    EventName = "UpdatingFieldOnPropertyWithNewValue",
    Message = "Updating {field} on twin {propertyId} with new value {propertyValue}")]
    public static partial void UpdatingFieldOnPropertyWithNewValue(this ILogger logger, string field, string propertyId, string propertyValue);

    [LoggerMessage(
    EventId = 2006,
    Level = LogLevel.Error,
    EventName = "PropertyMissingRequiredField",
    Message = "Property {propertyString} is missing required field {fieldName}")]
    public static partial void PropertyMissingRequiredField(this ILogger logger, string propertyString, string fieldName);

    [LoggerMessage(
    EventId = 2007,
    Level = LogLevel.Warning,
    EventName = "FactoryStreamingDataMissingRequiredData",
    Message = "{fieldName} is either missing or not of the right type in the streaming data payload for {streamingDataId}")]
    public static partial void FactoryStreamingDataMissingRequiredData(this ILogger logger, string fieldName, string streamingDataId);

    [LoggerMessage(
    EventId = 2008,
    Level = LogLevel.Debug,
    EventName = "FactoryStreamingDataCacheKeyLocked",
    Message = "The sampling rate has has been exhausted for counter key {key}. The key should clear after the key has expired.")]
    public static partial void FactoryStreamingDataCacheKeyLocked(this ILogger logger, string key);

    [LoggerMessage(
    EventId = 2009,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataOldTelemetryDataCached",
    Message = "An older telemetry sample ({streamingDataId}) made it through the sampling rate. Clearing key counter for {key}.")]
    public static partial void FactoryStreamingDataOldTelemetryDataCached(this ILogger logger, string streamingDataId, string key);

    [LoggerMessage(
    EventId = 2010,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataNewRandomSample",
    Message = "Cache key locked by sample {streamingDataId}. A new sample should be available in {samplingInterval} seconds.")]
    public static partial void FactoryStreamingDataNewRandomSample(this ILogger logger, string streamingDataId, int samplingInterval);

    [LoggerMessage(
    EventId = 2011,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataAdxEventProcessing",
    Message = "Generating ADX event data.")]
    public static partial void FactoryStreamingDataAdxEventProcessing(this ILogger logger);

    [LoggerMessage(
    EventId = 2012,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataAdxEventsCount",
    Message = "Finished generating ADX event data. Count of ADX event records - {recordsCount}.")]
    public static partial void FactoryStreamingDataAdxEventsCount(this ILogger logger, int recordsCount);

    [LoggerMessage(
    EventId = 2013,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataAdtEventProcessing",
    Message = "Generating ADT Patch operations related event data.")]
    public static partial void FactoryStreamingDataAdtEventProcessing(this ILogger logger);

    [LoggerMessage(
    EventId = 2014,
    Level = LogLevel.Information,
    EventName = "FactoryStreamingDataAdtEventProcessingDone",
    Message = "Finished generating ADT Patch operations related event data.")]
    public static partial void FactoryStreamingDataAdtEventProcessingDone(this ILogger logger);

    [LoggerMessage(
    EventId = 2015,
    Level = LogLevel.Debug,
    EventName = "FactoryStreamingDataConvertedToNull",
    Message = "The conversion to aas streaming data was found to be null for machine {machineId} with operation {operation} and model type {modelType}.")]
    public static partial void FactoryStreamingDataConvertedToNull(this ILogger logger, string machineId, string operation, string modelType);

    [LoggerMessage(
    EventId = 2016,
    Level = LogLevel.Debug,
    EventName = "FactoryStreamingUnsupportedOperationType",
    Message = "The operation type of {operation} is not supported for processing. Skipping this event.")]
    public static partial void FactoryStreamingUnsupportedOperationType(this ILogger logger, string operation);

    [LoggerMessage(
    EventId = 2017,
    Level = LogLevel.Warning,
    EventName = "FactoryStreamingDataCacheDependencyError",
    Message = "Failed to cache telemetry sample in Redis for machine {machineId}.")]
    public static partial void FactoryStreamingDataCacheDependencyError(this ILogger logger, Exception ex, string machineId);

    [LoggerMessage(
    EventId = 2018,
    Level = LogLevel.Debug,
    EventName = "FactoryStreamingDataIsNotValid",
    Message = "Streaming data is not valid. Skipping record...")]
    public static partial void FactoryStreamingDataIsNotValid(this ILogger logger);

    [LoggerMessage(
    EventId = 2019,
    Level = LogLevel.Warning,
    EventName = "ModelInstanceTypeDoesNotHaveSourceTimestampFieldConfigured",
    Message = "The model instance type {modelInstanceType} does not have the necessary mapping configured to get the source timestamp field name.")]
    public static partial void ModelInstanceTypeDoesNotHaveSourceTimestampFieldConfigured(this ILogger logger, ModelInstanceType modelInstanceType);

    [LoggerMessage(
    EventId = 2020,
    Level = LogLevel.Warning,
    EventName = "FactoryStreamingDataDoesNotContainValidSourceTimestampField",
    Message = "The streaming data for machine {machineId} and model type {modelInstanceType} does not contain a field with name {sourceTimestampFieldName}.")]
    public static partial void FactoryStreamingDataDoesNotContainValidSourceTimestampField(this ILogger logger, string machineId, ModelInstanceType modelInstanceType, string sourceTimestampFieldName);
}