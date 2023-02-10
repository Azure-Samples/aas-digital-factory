using AasFactory.Azure.Models.Factory.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The base model for the difference models within the object array.
    /// </summary>
    public class ModelInstance
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonRequired]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonRequired]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModelInstanceType ModelType { get; set; } = ModelInstanceType.Unknown;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonRequired]
        public string DisplayName { get; set; } = string.Empty;
    }
}