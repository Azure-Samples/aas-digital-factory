using Azure;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces
{
    public interface IBaseAdtRepository
    {
        void CreateOrReplaceRelationship<TRelationship>(string fromTwinId, string toTwinId)
            where TRelationship : BasicRelationship, new();

        void CreateOrReplaceTwin<TwinType>(TwinType twin)
            where TwinType : BasicDigitalTwin;

        Pageable<T> QueryTwins<T>(string query)
            where T : BasicDigitalTwin;

        Pageable<TRel> QueryRelationships<TRel>(string query)
            where TRel : BasicRelationship;
    }
}