using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models;

/// <summary>
/// The sub model mapping.
/// </summary>
public class SubModelMappingItem
{
    /// <summary>
    /// Gets or sets the sub model.
    /// </summary>
    public SubModelType SubModel { get; set; } = SubModelType.Unknown;

    /// <summary>
    /// Gets or sets the list of sub model elements. This is just a list of the attributes that can identify the Azure Event Hub field.
    /// </summary>
    public IEnumerable<SubModelElementMappingItem> SubModelElements { get; set; } = Enumerable.Empty<SubModelElementMappingItem>();
}