namespace AasFactory.Azure.Models.Factory.Enums
{
    /// <summary>
    /// Type of the variable described by the field.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Unknown type, should only be used when initializing a model instance.
        /// </summary>
        Unknown,

        /// <summary>
        /// Big int type.
        /// </summary>
        BigInt,

        /// <summary>
        /// Integer type.
        /// </summary>
        Boolean,

        /// <summary>
        /// Date time type.
        /// </summary>
        DateTime,

        /// <summary>
        /// Float32 type.
        /// </summary>
        Float32,

        /// <summary>
        /// Float64 type.
        /// </summary>
        Float64,

        /// <summary>
        /// Integer type.
        /// </summary>
        Int,

        /// <summary>
        /// String type.
        /// </summary>
        String,
    }
}