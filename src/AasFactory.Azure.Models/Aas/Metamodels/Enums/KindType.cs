namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of the kind.
/// </summary>
public enum KindType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The kind is a template.
    /// </summary>
    Template,

    /// <summary>
    /// The kind is an instance.
    /// </summary>
    Instance,
}