using System.Runtime.Serialization;

namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of level in data specification.
/// </summary>
public enum LevelType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The level type is minimum.
    /// </summary>
    [EnumMember(Value = "Min")]
    Minimum,

    /// <summary>
    /// The level type is maximum.
    /// </summary>
    [EnumMember(Value = "Max")]
    Maximum,

    /// <summary>
    /// The level type is number.
    /// </summary>
    [EnumMember(Value = "Nom")]
    Number,

    /// <summary>
    /// The level type is typical.
    /// </summary>
    [EnumMember(Value = "Typ")]
    Typical,
}