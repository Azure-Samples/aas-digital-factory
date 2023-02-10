using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The line model.
    /// </summary>
    public class Line : ModelInstance
    {
        /// <summary>
        /// Gets or sets the name the line is under.
        /// </summary>
        [JsonRequired]
        public IdentifiableInstance Factory { get; set; } = new IdentifiableInstance();

        /// <summary>
        /// Gets or sets the line topology.
        /// </summary>
        [JsonRequired]
        public IEnumerable<LineTopology> Topology { get; set; } = Enumerable.Empty<LineTopology>();
    }
}
