namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of the asset kind.
/// </summary>
public enum AssetKindType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The asset kind is a type.
    /// </summary>
    Type,

    /// <summary>
    /// The asset kind is an instance.
    /// </summary>
    Instance,
}