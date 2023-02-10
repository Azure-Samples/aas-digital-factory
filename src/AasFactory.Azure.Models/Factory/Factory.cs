using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The factory model.
    /// </summary>
    public class Factory : ModelInstance
    {
        /// <summary>
        /// Gets or sets the place name.
        /// </summary>
        [JsonRequired]
        public string PlaceName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        [JsonRequired]
        public string TimeZone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of machines that are not associated with a line.
        /// </summary>
        [JsonRequired]
        public IEnumerable<IdentifiableInstance> Machines { get; set; } = Enumerable.Empty<IdentifiableInstance>();

        /// <summary>
        /// Gets or sets the list of lines for the factory.
        /// </summary>
        [JsonRequired]
        public IEnumerable<IdentifiableInstance> Lines { get; set; } = Enumerable.Empty<IdentifiableInstance>();
    }
}