using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class DataSpecification
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Id Short.
    /// </summary>
    public string IdShort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Definition.
    /// </summary>
    public LangStringSet Definition { get; set; } = new ();

    /// <summary>
    /// Gets or sets the preferred name.
    /// </summary>
    public LangStringSet PreferredName { get; set; } = new ();

    /// <summary>
    /// Gets or sets the short name.
    /// </summary>
    public LangStringSet ShortName { get; set; } = new ();

    /// <summary>
    /// Gets or sets the data type.
    /// </summary>
    public DataType DataType { get; set; } = DataType.None;

    /// <summary>
    /// Gets or sets the level type.
    /// </summary>
    public LevelType LevelType { get; set; } = LevelType.None;

    /// <summary>
    /// Gets or sets the source of definition.
    /// </summary>
    public string SourceOfDefinition { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the symbol.
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit.
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit id value.
    /// </summary>
    public string UnitIdValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value format.
    /// </summary>
    public string ValueFormat { get; set; } = string.Empty;
}