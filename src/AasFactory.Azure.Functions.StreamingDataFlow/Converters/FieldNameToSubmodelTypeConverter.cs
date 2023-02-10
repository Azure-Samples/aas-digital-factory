using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Converters;

public class FieldNameToSubmodelTypeConverter : IAdapter<(string fieldName, ModelInstanceType modelInstanceType), SubModelType>
{
    /// <inheritdoc />
    public SubModelType Convert((string fieldName, ModelInstanceType modelInstanceType) tuple)
    {
        if (!AasMappingConfig.Mapping.TryGetValue(tuple.modelInstanceType, out var mappingContainer))
        {
            return SubModelType.Unknown;
        }

        var fieldNameLowered = tuple.fieldName.ToLower();
        foreach (var subModelMapping in mappingContainer.SubModels)
        {
            var subModelContainsFieldName = subModelMapping
                .SubModelElements.Any(elem =>
                    string.Equals(elem.Name.ToLower(), fieldNameLowered));

            if (subModelContainsFieldName)
            {
                return subModelMapping.SubModel;
            }
        }

        return mappingContainer.DefaultSubModel;
    }
}
