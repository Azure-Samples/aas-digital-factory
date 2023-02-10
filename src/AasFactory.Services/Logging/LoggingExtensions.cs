using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace AasFactory.Services.Logging;

[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessage(
    EventId = 500,
    Level = LogLevel.Debug,
    EventName = "CreatingTwin",
    Message = "Creating or replacing twin id - {twinId}")]
    public static partial void CreatingTwin(this ILogger logger, string twinId);

    [LoggerMessage(
    EventId = 501,
    Level = LogLevel.Error,
    EventName = "FailedToCreateTwin",
    Message = "Failed to create twin id - {twinId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToCreateTwin(this ILogger logger, string twinId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 502,
    Level = LogLevel.Error,
    EventName = "FailedToDeleteTwin",
    Message = "Failed to delete twin id - {twinId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToDeleteTwin(this ILogger logger, string twinId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 503,
    Level = LogLevel.Error,
    EventName = "FailedToUpdateTwin",
    Message = "Failed to update twin id - {twinId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToUpdateTwin(this ILogger logger, string twinId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 504,
    Level = LogLevel.Error,
    EventName = "FailedToDeleteRelationship",
    Message = "Failed deleting relationship: twinId - {twinId}, relationship Id - {relationshipId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToDeleteRelationship(this ILogger logger, string twinId, string relationshipId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 505,
    Level = LogLevel.Error,
    EventName = "FailedToCreateRelationship",
    Message = "Failed to create a relationship: twinId - {twinId}, relationship Id - {relationshipId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToCreateRelationship(this ILogger logger, string twinId, string relationshipId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 506,
    Level = LogLevel.Error,
    EventName = "FailedToUpdateRelationship",
    Message = "Failed to update a relationship: twinId - {twinId}, relationship Id - {relationshipId}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToUpdateRelationship(this ILogger logger, string twinId, string relationshipId, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 507,
    Level = LogLevel.Debug,
    EventName = "DownloadingFromBlob",
    Message = "starting download from '{blobContainerName}/{blobName}")]
    public static partial void DownloadingFromBlob(this ILogger logger, string blobContainerName, string blobName);

    [LoggerMessage(
    EventId = 508,
    Level = LogLevel.Information,
    EventName = "DownloadedFromBlob",
    Message = "successfully downloaded {blobLength} bytes from '{blobContainerName}/{blobName}' after {elapsedMilliseconds} ms.")]
    public static partial void DownloadedFromBlob(this ILogger logger, int blobLength, string blobContainerName, string blobName, long elapsedMilliseconds);

    [LoggerMessage(
    EventId = 509,
    Level = LogLevel.Information,
    EventName = "UploadedToBlob",
    Message = "successfully uploaded to '{blobContainerName}/{blobName}' after {elapsedMilliseconds} ms.")]
    public static partial void UploadedToBlob(this ILogger logger, string blobContainerName, string blobName, long elapsedMilliseconds);

    [LoggerMessage(
    EventId = 510,
    Level = LogLevel.Debug,
    EventName = "UploadingToBlob",
    Message = "Start uploading {blobLength} bytes to '{blobContainerName}/{blobName}'.")]
    public static partial void UploadingToBlob(this ILogger logger, int blobLength, string blobContainerName, string blobName);

    [LoggerMessage(
    EventId = 511,
    Level = LogLevel.Debug,
    EventName = "DeletingBlob",
    Message = "Start deleting of '{blobContainerName}/{blobName}'.")]
    public static partial void DeletingBlob(this ILogger logger, string blobContainerName, string blobName);

    [LoggerMessage(
    EventId = 512,
    Level = LogLevel.Information,
    EventName = "UploadedToBlob",
    Message = "Deleted '{blobContainerName}/{blobName}' after {elapsedMilliseconds} ms.")]
    public static partial void DeletedBlob(this ILogger logger, string blobContainerName, string blobName, long elapsedMilliseconds);

    [LoggerMessage(
    EventId = 513,
    Level = LogLevel.Error,
    EventName = "FailedToQueryTwins",
    Message = "Failed to query twins. Query - {query}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToQueryTwins(this ILogger logger, string query, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 514,
    Level = LogLevel.Error,
    EventName = "FailedToQueryRelationships",
    Message = "Failed to query relationships. Query - {query}, errorCode - {errorCode}, statusCode - {statusCode}, Failure Reason - {message}")]
    public static partial void FailedToQueryRelationships(this ILogger logger, string query, string errorCode, string statusCode, string message);

    [LoggerMessage(
    EventId = 515,
    Level = LogLevel.Information,
    EventName = "GraphTotalTwins",
    Message = "Count of twins on the graph: {twinsCount}")]
    public static partial void GraphTotalTwins(this ILogger logger, int twinsCount);

    [LoggerMessage(
    EventId = 516,
    Level = LogLevel.Information,
    EventName = "GraphTotalRelationships",
    Message = "Count of relationships on the graph : {relationshipsCount}")]
    public static partial void GraphTotalRelationships(this ILogger logger, int relationshipsCount);

    [LoggerMessage(
    EventId = 517,
    Level = LogLevel.Information,
    EventName = "DeletingTwins",
    Message = "Count of twins to be deleted : {twinsCount}")]
    public static partial void DeletingTwins(this ILogger logger, int twinsCount);

    [LoggerMessage(
    EventId = 518,
    Level = LogLevel.Information,
    EventName = "DeletingRelationships",
    Message = "Count of relationships to be deleted : {relationshipsCount}")]
    public static partial void DeletingRelationships(this ILogger logger, int relationshipsCount);

    [LoggerMessage(
    EventId = 519,
    Level = LogLevel.Information,
    EventName = "TotalTwinsRequestedInThisRun",
    Message = "Count of twins requested to be created/replaced in this run: {twinsCount}")]
    public static partial void TotalTwinsRequestedInThisRun(this ILogger logger, int twinsCount);

    [LoggerMessage(
    EventId = 520,
    Level = LogLevel.Information,
    EventName = "TotalRelationshipsRequestedInThisRun",
    Message = "Count of relationships requested to be created/replaced in this run: {relationshipsCount}")]
    public static partial void TotalRelationshipsRequestedInThisRun(this ILogger logger, int relationshipsCount);

    [LoggerMessage(
    EventId = 521,
    Level = LogLevel.Debug,
    EventName = "SuccessfullyCreatedTwin",
    Message = "Successfully created twin {twinType} with Id {twinId}")]
    public static partial void SuccessfullyCreatedTwin(this ILogger logger, string twinType, string twinId);

    [LoggerMessage(
        EventId = 522,
        Level = LogLevel.Debug,
        EventName = "SuccessfullyCreatedRelationship",
        Message = "Successfully created {nameOfRelationship} relationship from {sourceTwinId} to {targetTwinId} Ids")]
    public static partial void SuccessfullyCreatedRelationship(this ILogger logger, string nameOfRelationship, string sourceTwinId, string targetTwinId);

    [LoggerMessage(
    EventId = 523,
    Level = LogLevel.Debug,
    EventName = "SuccessfullyDeletedTwin",
    Message = "Successfully deleted twin Id : {twinId}")]
    public static partial void SuccessfullyDeletedTwin(this ILogger logger, string twinId);

    [LoggerMessage(
    EventId = 524,
    Level = LogLevel.Debug,
    EventName = "SuccessfullyDeletedRelationship",
    Message = "Successfully deleted relationship Id : {relationshipId}")]
    public static partial void SuccessfullyDeletedRelationship(this ILogger logger, string relationshipId);
}
