using AasFactory.Azure.Models.Factory.Enums;
using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Factory
{
    /// <summary>
    /// This class represents an instance of the machine type field of the <see cref="MachineTypeField" /> class.
    /// </summary>
    public class MachineTypeField
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

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonRequired]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the data type.
        /// </summary>
        [JsonRequired]
        public DataType DataType { get; set; } = DataType.Unknown;

        /// <summary>
        /// Gets or sets the stat type (ordinal, nominal, etc.).
        /// </summary>
        public StatType StatType { get; set; } = StatType.None;

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        public string Unit { get; set; } = string.Empty;
    }
}