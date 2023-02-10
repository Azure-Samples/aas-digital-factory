using Azure;
using Azure.DigitalTwins.Core;

namespace AasFactory.Services;

public interface IAdtClient
{
    void CreateOrReplaceTwin<T>(string twinId, T twin)
    where T : BasicDigitalTwin;

    Task UpdateTwinAsync(string twinId, JsonPatchDocument patchDocument);

    void DeleteTwin(string twinId);

    void DeleteRelationship(string twinId, string relationshipId);

    void CreateOrReplaceRelationship<TRel>(string twinId, string relationshipId, TRel relationship)
    where TRel : BasicRelationship;

    void UpdateRelationship(string twinId, string relationshipId,  JsonPatchDocument patchDocument);

    Pageable<T> QueryTwins<T>(string query)
    where T : BasicDigitalTwin;

    Pageable<TRel> QueryRelationships<TRel>(string query)
    where TRel : BasicRelationship;
}
