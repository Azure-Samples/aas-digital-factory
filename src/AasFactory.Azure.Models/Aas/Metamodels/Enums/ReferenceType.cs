namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the reference type.
/// </summary>
public enum ReferenceType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The reference type is a global reference.
    /// </summary>
    GlobalReference,

    /// <summary>
    /// The reference type is a model reference.
    /// </summary>
    ModelReference,
}