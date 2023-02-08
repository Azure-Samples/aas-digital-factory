using Microsoft.Extensions.Configuration;

namespace AasFactory.EventHubSimulator;

class Config
{
    private readonly IConfiguration configuration;
    public Config(IConfiguration configuration)
    {
        this.configuration = configuration;

        this.EventHubConnectionString = this.TryGetStringConfigValue("EVENT_HUB_CONNECTION_STRING");
        this.EventHubName = this.TryGetStringConfigValue("EVENT_HUB_NAME");
        this.BlobStorageConnectionString = this.TryGetStringConfigValue("BLOB_STORAGE_CONNECTION_STRING");
        this.BlobStorageContainer = this.TryGetStringConfigValue("BLOB_STORAGE_CONTAINER");
        this.BlobStorageBlobPath = this.TryGetStringConfigValue("BLOB_STORAGE_BLOB_PATH");
        this.TimeBetweenEventsInSeconds = this.configuration.GetValue<int>("TIME_BETWEEN_EVENTS_IN_SECONDS", 30);
    }

    private string TryGetStringConfigValue(string key)
    {
        var configValue = this.configuration.GetValue<string>(key);
        if (string.IsNullOrEmpty(configValue))
        {
            throw new Exception($"{key} must be defined");
        }

        return configValue;
    }

    /// <summary>
    /// Gets or sets the event hub connection string.
    /// </summary>
    public string EventHubConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event hub name.
    /// </summary>
    public string EventHubName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the blob storage connection string.
    /// </summary>
    public string BlobStorageConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the blob storage container.
    /// </summary>

    public string BlobStorageContainer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the blob storage blob path.
    /// </summary>
    public string BlobStorageBlobPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sampling rate in seconds.
    /// </summary>
    public int TimeBetweenEventsInSeconds { get; set; }
}