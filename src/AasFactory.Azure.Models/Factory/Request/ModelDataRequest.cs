using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory.Request
{
    /// <summary>
    /// The initial blob payload.
    /// </summary>
    public class ModelDataRequest
    {
        /// <summary>
        /// Gets or sets the list of factories.
        /// </summary>
        [JsonRequired]
        public IEnumerable<Factory> Factory { get; set; } = Enumerable.Empty<Factory>();

        /// <summary>
        /// Gets or sets the list of lines.
        /// </summary>
        [JsonRequired]
        public IEnumerable<Line> Line { get; set; } = Enumerable.Empty<Line>();

        /// <summary>
        /// Gets or sets the list of machines.
        /// </summary>
        [JsonRequired]
        public IEnumerable<Machine> Machine { get; set; } = Enumerable.Empty<Machine>();

        /// <summary>
        /// Gets or sets the list of machine types for a specific sink.
        /// </summary>
        [JsonRequired]
        public IEnumerable<MachineType> MachineType { get; set; } = Enumerable.Empty<MachineType>();
    }
}