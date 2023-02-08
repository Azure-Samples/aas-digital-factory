using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.ModelDataFlow.Logger;

[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Debug,
        EventName = "CreatingRelationship",
        Message = "Create {nameOfRelationship} relationship from {sourceTwinId} to {targetTwinId} Ids")]
    public static partial void CreatingRelationship(this ILogger logger, string nameOfRelationship, string sourceTwinId, string targetTwinId);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Debug,
        EventName = "CreatingTwin",
        Message = "Creating twin {twinType} with Id {twinId}")]
    public static partial void CreatingTwin(this ILogger logger, string twinType, string twinId);

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Error,
        EventName = "InvalidPropertyType",
        Message = "Failed to create a property - {propertyId} because the property value type {propertyType} is not allowed.")]
    public static partial void InvalidPropertyType(this ILogger logger, string propertyId, string propertyType);

    [LoggerMessage(
    EventId = 1005,
    Level = LogLevel.Debug,
    EventName = "DownloadingFactoryData",
    Message = "Downloading and deserializing {blobPath}...")]
    public static partial void DownloadingFactoryData(this ILogger logger, string blobPath);

    [LoggerMessage(
    EventId = 1006,
    Level = LogLevel.Debug,
    EventName = "SuccessfullyDownloadedFactoryData",
    Message = "Successfully downloaded and deserialized the payload - {blobPath} after {elapsedTime} ms.")]
    public static partial void SuccessfullyDownloadedFactoryData(this ILogger logger, string blobPath, TimeSpan elapsedTime);

    [LoggerMessage(
    EventId = 1007,
    Level = LogLevel.Information,
    EventName = "SuccessfullyConvertedFactoryData",
    Message = "Successfully converted models, serialized AAS models and saving to blob storage after {elapsedTime} ms.")]
    public static partial void SuccessfullyConvertedFactoryData(this ILogger logger, long elapsedTime);

    [LoggerMessage(
    EventId = 1008,
    Level = LogLevel.Information,
    EventName = "ConvertingToAasModel",
    Message = "Converting models to AAS, serializing AAS models and saving to blob storage.")]
    public static partial void ConvertingToAasModel(this ILogger logger);

    [LoggerMessage(
    EventId = 1009,
    Level = LogLevel.Error,
    EventName = "FailedToUploadAas",
    Message = "There was an error uploading the shells - {aasBlobPath}")]
    public static partial void FailedToUploadAas(this ILogger logger, Exception ex, string aasBlobPath);

    [LoggerMessage(
    EventId = 1010,
    Level = LogLevel.Error,
    EventName = "FailedToReadBlob",
    Message = "There was an error downloading and deserializing the blob.")]
    public static partial void FailedToReadBlob(this ILogger logger, Exception ex);

    [LoggerMessage(
    EventId = 1011,
    Level = LogLevel.Error,
    EventName = "FailedToBuildShells",
    Message = "There was an error building and serializing the shells.")]
    public static partial void FailedToBuildShells(this ILogger logger, Exception ex);

    [LoggerMessage(
    EventId = 1012,
    Level = LogLevel.Information,
    EventName = "ProcessedAasToAdt",
    Message = "AAS models to ADT flow is marked done.")]
    public static partial void ProcessedAasToAdt(this ILogger logger);

    [LoggerMessage(
    EventId = 1013,
    Level = LogLevel.Information,
    EventName = "BuiltAasShells",
    Message = "Built AasShells with {factoryCount} factories, {machineTypeCount} machineTypes, {lineCount} lines, {machineCount} machines, {cdCount} concept descriptions.")]
    public static partial void BuiltAasShells(this ILogger logger, int factoryCount, int machineTypeCount, int lineCount, int machineCount, int cdCount);

    [LoggerMessage(
    EventId = 1014,
    Level = LogLevel.Debug,
    EventName = "DownloadedAasContent",
    Message = "Content of download AAS format: {aasContent}")]
    public static partial void DownloadedAasContent(this ILogger logger, string aasContent);

    [LoggerMessage(
    EventId = 1015,
    Level = LogLevel.Debug,
    EventName = "DownloadedAasContent",
    Message = "Converting factory with id: {factoryId}, name: {factoryName}")]
    public static partial void ConvertingFactoryData(this ILogger logger, string factoryId, string factoryName);

    [LoggerMessage(
    EventId = 1016,
    Level = LogLevel.Debug,
    EventName = "ConvertingLineData",
    Message = "Converting line with id: {lineId}, name: {lineName}")]
    public static partial void ConvertingLineData(this ILogger logger, string lineId, string lineName);

    [LoggerMessage(
    EventId = 1017,
    Level = LogLevel.Debug,
    EventName = "ConvertingMachineData",
    Message = "Converting machine with id: {machineId}, name: {machineName}")]
    public static partial void ConvertingMachineData(this ILogger logger, string machineId, string machineName);

    [LoggerMessage(
    EventId = 1018,
    Level = LogLevel.Debug,
    EventName = "ConvertingMachineTypeData",
    Message = "Converting machine type with id: {machineTypeId}, name: {machineTypeName}")]
    public static partial void ConvertingMachineTypeData(this ILogger logger, string machineTypeId, string machineTypeName);

    [LoggerMessage(
    EventId = 1019,
    Level = LogLevel.Debug,
    EventName = "CreateConceptDescription",
    Message = "Converting machine type field to concept description. field id: {fieldId}, field name: {fieldName}, type id: {typeId}, type name: {typeName}")]
    public static partial void ConvertingConceptDescription(this ILogger logger, string fieldId, string fieldName, string typeId, string typeName);

    [LoggerMessage(
    EventId = 1020,
    Level = LogLevel.Information,
    EventName = "QueryTwinsInGraph",
    Message = "Querying the graph to get all the existing twins: {query}")]
    public static partial void QueryTwinsInGraph(this ILogger logger, string query);

    [LoggerMessage(
    EventId = 1021,
    Level = LogLevel.Information,
    EventName = "QueryRelationshipsInGraph",
    Message = "Querying the graph to get all the existing relationships: {query}")]
    public static partial void QueryRelationshipsInGraph(this ILogger logger, string query);

    [LoggerMessage(
    EventId = 1022,
    Level = LogLevel.Debug,
    EventName = "DeletingTwin",
    Message = "Deleting twin Id : {twinId}")]
    public static partial void DeletingTwin(this ILogger logger, string twinId);

    [LoggerMessage(
    EventId = 1024,
    Level = LogLevel.Debug,
    EventName = "DeletingRelationship",
    Message = "Deleting relationship Id : {relationshipId}")]
    public static partial void DeletingRelationship(this ILogger logger, string relationshipId);

    [LoggerMessage(
    EventId = 1025,
    Level = LogLevel.Error,
    EventName = "FailedToBuildgraph",
    Message = "There is an error while building the graph. Execution has been terminated and logged errors need to be addressed to have a successful attempt at building the graph.")]
    public static partial void FailedToBuildgraph(this ILogger logger, Exception ex);

    [LoggerMessage(
    EventId = 1026,
    Level = LogLevel.Information,
    EventName = "CreatingOrReplacingTwinsAndRelationships",
    Message = "** Started creating or replacing twins and relationships on the ADT instance **")]
    public static partial void CreatingOrReplacingTwinsAndRelationships(this ILogger logger);

    [LoggerMessage(
    EventId = 1027,
    Level = LogLevel.Information,
    EventName = "DoneCreatingOrReplacingTwinsAndRelationships",
    Message = "** Done creating or replacing valid twins and relationships on the ADT instance **")]
    public static partial void DoneCreatingOrReplacingTwinsAndRelationships(this ILogger logger);

    [LoggerMessage(
    EventId = 1028,
    Level = LogLevel.Information,
    EventName = "DeletingTwinsAndRelationships",
    Message = "** Started deleting unwanted twins and relationships on the ADT instance **")]
    public static partial void DeletingTwinsAndRelationships(this ILogger logger);

    [LoggerMessage(
    EventId = 1029,
    Level = LogLevel.Information,
    EventName = "DoneDeletingTwinsAndRelationships",
    Message = "**Done deleting unwanted twins and relationships on the ADT instance **")]
    public static partial void DoneDeletingTwinsAndRelationships(this ILogger logger);

    [LoggerMessage(
    EventId = 1030,
    Level = LogLevel.Information,
    EventName = "BuildingAasShells",
    Message = "Starting to build AAS Shells...")]
    public static partial void BuildingAasShells(this ILogger logger);
}