using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The identifiable instance model contains the name and id of a given model instance.
    /// </summary>
    public class IdentifiableInstance
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonRequired]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; } = string.Empty;
    }
}
