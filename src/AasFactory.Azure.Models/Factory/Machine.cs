using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The machine model.
    /// </summary>
    public class Machine : ModelInstance
    {
        /// <summary>
        /// Gets or sets the machine type.
        /// </summary>
        [JsonRequired]
        public IdentifiableInstance MachineType { get; set; } = new ();

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        [JsonRequired]
        public IdentifiableInstance Factory { get; set; } = new ();

        /// <summary>
        /// Gets or sets the line the machine is under.
        /// </summary>
        public IEnumerable<IdentifiableInstance> Lines { get; set; } = Enumerable.Empty<IdentifiableInstance>();
    }
}
