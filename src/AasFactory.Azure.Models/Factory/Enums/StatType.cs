namespace AasFactory.Azure.Models.Factory.Enums
{
    /// <summary>
    /// Type of the variable described by the field.
    /// </summary>
    public enum StatType
    {
        /// <summary>
        /// None type, should only be used when initializing a model instance.
        /// </summary>
        None,

        /// <summary>
        /// Continuous type.
        /// </summary>
        Continuous,

        /// <summary>
        /// Nominal type.
        /// </summary>
        Nominal,

        /// <summary>
        /// Ordinal type.
        /// </summary>
        Ordinal,
    }
}