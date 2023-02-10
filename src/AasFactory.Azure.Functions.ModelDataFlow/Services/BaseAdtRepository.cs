using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Services;
using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

public class BaseAdtRepository : IBaseAdtRepository
{
    private readonly IAdtClient sdkClient;
    private readonly IAdtRelationshipIdBuilder idBuilder;
    private readonly IAdtTracker adtTracker;

    public BaseAdtRepository(IAdtHandler adtHandler, IModelDataFlowSettings settings, ILogger logger, IAdtTracker adtTracker, IAdtRelationshipIdBuilder idBuilder)
    {
        this.sdkClient = adtHandler.GetAdtClient(settings.DigitalTwinsInstanceUrl, settings.ContinueOnAdtErrors);
        this.Logger = logger;
        this.adtTracker = adtTracker;
        this.idBuilder = idBuilder;
    }

    protected ILogger Logger { get; private set; }

    public void CreateOrReplaceTwin<TwinType>(TwinType twin)
        where TwinType : BasicDigitalTwin
    {
        this.Logger.CreatingTwin(twin.GetType().Name, twin.Id);
        this.adtTracker.AddTwinId(twin.Id);
        this.sdkClient.CreateOrReplaceTwin(twin.Id, twin);
    }

    public void CreateOrReplaceRelationship<TRelationship>(string fromTwinId, string toTwinId)
        where TRelationship : BasicRelationship, new()
    {
        var nameOfRelationship = nameof(TRelationship);
        this.Logger.CreatingRelationship(nameOfRelationship, fromTwinId, toTwinId);
        var valueRel = new TRelationship
        {
            SourceId = fromTwinId,
            Id = this.idBuilder.BuildRelationshipId(fromTwinId, toTwinId),
            TargetId = toTwinId,
        };
        this.adtTracker.AddRelationshipId(valueRel.Id);
        this.sdkClient.CreateOrReplaceRelationship<TRelationship>(valueRel.SourceId, valueRel.Id, valueRel);
    }

    public void DeleteTwin(string twinId)
    {
        this.Logger.DeletingTwin(twinId);
        this.sdkClient.DeleteTwin(twinId);
    }

    public void DeleteRelationship(string twinId, string relationshipId)
    {
        this.Logger.DeletingRelationship(relationshipId);
        this.sdkClient.DeleteRelationship(twinId, relationshipId);
    }

    public Pageable<T> QueryTwins<T>(string query)
    where T : BasicDigitalTwin
    {
        this.Logger.QueryTwinsInGraph(query);
        return this.sdkClient.QueryTwins<T>(query);
    }

    public Pageable<TRel> QueryRelationships<TRel>(string query)
    where TRel : BasicRelationship
    {
        this.Logger.QueryRelationshipsInGraph(query);
        return this.sdkClient.QueryRelationships<TRel>(query);
    }
}
