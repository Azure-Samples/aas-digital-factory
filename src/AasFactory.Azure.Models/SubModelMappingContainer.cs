using AasFactory.Azure.Models.Aas.Metamodels.Enums;

namespace AasFactory.Azure.Models;

/// <summary>
/// The container for the sub model mapping.
/// </summary>
public class SubModelMappingContainer
{
    /// <summary>
    /// Gets or sets the list of sub model mapping items.
    /// </summary>
    public IEnumerable<SubModelMappingItem> SubModels { get; set; } = Enumerable.Empty<SubModelMappingItem>();

    /// <summary>
    /// Gets or sets the default sub model.
    /// </summary>
    public SubModelType DefaultSubModel { get; set; } = SubModelType.Unknown;

    /// <summary>
    /// Gets or sets the source timestamp field name. This field is used to keep the order for historization purposes.
    /// For ranges this is typically start time and for moment in time data this is typically timestamp.
    /// </summary>
    public string SourceTimestampFieldName { get; set; } = string.Empty;
}