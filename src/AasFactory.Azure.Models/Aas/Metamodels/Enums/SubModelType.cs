namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the different sub model types.
/// </summary>
public enum SubModelType
{
    /// <summary>
    /// Unknown type, this is the default type.
    /// </summary>
    Unknown,

    /// <summary>
    /// The documentation sub model type.
    /// </summary>
    Documentation,

    /// <summary>
    /// The lines sub model type.
    /// </summary>
    Lines,

    /// <summary>
    /// The machines sub model type.
    /// </summary>
    Machines,

    /// <summary>
    /// The nameplate sub model type.
    /// </summary>
    Nameplate,

    /// <summary>
    /// The operational data sub model type.
    /// </summary>
    OperationalData,

    /// <summary>
    /// The process flow (line) sub model type.
    /// </summary>
    ProcessFlow,

    /// <summary>
    /// The technical data sub model type.
    /// </summary>
    TechnicalData,
}