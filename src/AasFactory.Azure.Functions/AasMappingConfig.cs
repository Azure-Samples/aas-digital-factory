using AasFactory.Azure.Models;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions;

public static class AasMappingConfig
{
    /// <summary>
    /// Get the sub model mapping.
    /// </summary>
    public static IDictionary<ModelInstanceType, SubModelMappingContainer> Mapping => new Dictionary<ModelInstanceType, SubModelMappingContainer>()
    {
        [ModelInstanceType.MachineType] = new SubModelMappingContainer() // cycle
        {
            DefaultSubModel = SubModelType.OperationalData,
            SourceTimestampFieldName = "starttime",
        },
    };
}