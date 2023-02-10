using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// This class represents a portion of the line topology within the <see cref="Line"/> class.
    /// </summary>
    public class LineTopology
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonRequired]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the machine name.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list or machines that are successors to the machine.
        /// </summary>
        public IEnumerable<IdentifiableInstance> Successors { get; set; } = Enumerable.Empty<IdentifiableInstance>();

        /// <summary>
        /// Gets or sets the list or machines that are predecessors to the machine.
        /// </summary>
        public IEnumerable<IdentifiableInstance> Predecessors { get; set; } = Enumerable.Empty<IdentifiableInstance>();
    }
}
