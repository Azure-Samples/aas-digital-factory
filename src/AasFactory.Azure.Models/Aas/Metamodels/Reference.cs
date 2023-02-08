using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models.Aas.Metamodels;

/// <summary>
/// The Reference Element AAS Component.
/// </summary>
[ExcludeFromCodeCoverage]
public class Reference
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets key 1.
    /// </summary>
    public Key Key1 => new ();

    /// <summary>
    /// Gets key 2.
    /// </summary>
    public Key Key2 => new ();

    /// <summary>
    /// Gets key 3.
    /// </summary>
    public Key Key3 => new ();

    /// <summary>
    /// Gets key 4.
    /// </summary>
    public Key Key4 => new ();

    /// <summary>
    /// Gets key 5.
    /// </summary>
    public Key Key5 => new ();

    /// <summary>
    /// Gets key 6.
    /// </summary>
    public Key Key6 => new ();

    /// <summary>
    /// Gets key 7.
    /// </summary>
    public Key Key7 => new ();

    /// <summary>
    /// Gets key 8.
    /// </summary>
    public Key Key8 => new ();

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public ReferenceType Type { get; set; } = ReferenceType.None;
}