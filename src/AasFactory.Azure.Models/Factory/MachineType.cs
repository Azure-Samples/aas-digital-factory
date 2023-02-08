using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// The machine type model.
    /// </summary>
    public class MachineType : ModelInstance
    {
        /// <summary>
        /// Gets or sets the list of machine type fields.
        /// </summary>
        [JsonRequired]
        public IEnumerable<MachineTypeField> Fields { get; set; } = Enumerable.Empty<MachineTypeField>();
    }
}
