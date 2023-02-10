using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Models.Adt.Relationships;
using AasFactory.Services;
using Microsoft.Extensions.Logging;
using Aas = AasFactory.Azure.Models.Aas.Metamodels;
using Twins = AasFactory.Azure.Models.Adt.Twins;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

/// <inheritdoc cref="IConceptDescriptionRepository"/>
public class ConceptDescriptionRepository : BaseAdtRepository, IConceptDescriptionRepository
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConceptDescriptionRepository"/>.
    /// </summary>
    /// <param name="adtHandler"></param>
    /// <param name="modelDataFlowSettings"></param>
    /// <param name="logger"></param>
    /// <param name="adtTracker"></param>
    /// <param name="idBuilder"></param>
    public ConceptDescriptionRepository(
        IAdtHandler adtHandler,
        IModelDataFlowSettings modelDataFlowSettings,
        ILogger<IConceptDescriptionRepository> logger,
        IAdtTracker adtTracker,
        IAdtRelationshipIdBuilder idBuilder)
        : base(adtHandler, modelDataFlowSettings, logger, adtTracker, idBuilder)
    {
    }

    /// <inheritdoc />
    public void CreateOrReplaceConceptDescription(Aas.ConceptDescription conceptDescription)
    {
        var conceptDescriptionTwin = new Twins.ConceptDescription(conceptDescription);
        var dataSpecificationTwin = new Twins.DataSpecification(conceptDescription.DataSpecification);

        this.CreateOrReplaceTwin(conceptDescriptionTwin);
        this.CreateOrReplaceTwin(dataSpecificationTwin);
        this.CreateOrReplaceRelationship<ConceptDescriptionToDataSpecification>(conceptDescription.Id, dataSpecificationTwin.Id);

        foreach (var referenceElementId in conceptDescription.ReferenceElementIds)
        {
            // ReferenceElement is created by machine type
            this.CreateOrReplaceRelationship<ReferenceToConceptDescription>(referenceElementId, conceptDescription.Id);
        }
    }
}