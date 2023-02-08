using AasFactory.Azure.Models.EventHubs;
using Newtonsoft.Json;

public class ModelDataEventChanged : IntegrationEvent
    {
        /// <summary>
        /// Gets or sets the path to the data file in blob storage.
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }