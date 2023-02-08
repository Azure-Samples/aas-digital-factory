namespace AasFactory.Azure.Models.Factory.Enums
{
    /// <summary>
    /// Type of model instance types.
    /// </summary>
    public enum ModelInstanceType
    {
        /// <summary>
        /// Unknown type, should only be used when initializing a model instance.
        /// </summary>
        Unknown,

        /// <summary>
        /// Factory type, describes a collection of lines and machines.
        /// </summary>
        Factory,

        /// <summary>
        /// Line type, describes a collection of interconnected machines.
        /// </summary>
        Line,

        /// <summary>
        /// Machine type, describes an individual machine.
        /// </summary>
        Machine,

        /// <summary>
        /// Machine Type type, describes fields belonging to a machine.
        /// </summary>
        MachineType,
    }
}
