using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Converters;

public class ModelInstanceTypeToSourceTimestampFieldNameConverter : IAdapter<ModelInstanceType, string?>
{
    /// <inheritdoc />
    public string? Convert(ModelInstanceType modelInstanceType)
    {
        if (AasMappingConfig.Mapping.TryGetValue(modelInstanceType, out var mappingContainer))
        {
            return mappingContainer.SourceTimestampFieldName;
        }

        return null;
    }
}