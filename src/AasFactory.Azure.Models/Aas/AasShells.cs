using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Models.Aas;

/// <summary>
/// The container for all the AAS objects.
/// </summary>
[ExcludeFromCodeCoverage]
public class AasShells
{
    /// <summary>
    /// Gets or sets the list of Factory Shells.
    /// </summary>
    public IEnumerable<Shell> Factories { get; set; } = Enumerable.Empty<Shell>();

    /// <summary>
    /// Gets or sets the list of Machine Shells.
    /// </summary>
    public IEnumerable<Shell> Machines { get; set; } = Enumerable.Empty<Shell>();

    /// <summary>
    /// Gets or sets the list of Line Shells.
    /// </summary>
    public IEnumerable<Shell> Lines { get; set; } = Enumerable.Empty<Shell>();

    /// <summary>
    /// Gets or sets the list of MachineType Shells.
    /// </summary>
    public IEnumerable<Shell> MachineTypes { get; set; } = Enumerable.Empty<Shell>();

    /// <summary>
    /// Gets or sets the list of Concept Description components.
    /// </summary>
    public IEnumerable<ConceptDescription> ConceptDescriptions { get; set; } = Enumerable.Empty<ConceptDescription>();
}