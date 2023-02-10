using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Services;
using AasFactory.Services.Logging;
using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;
using Aas = AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services
{
    /// <inheritdoc cref="IGraphRepository"/>
    public class GraphRepository : BaseAdtRepository, IGraphRepository
    {
        private readonly IAdtTracker adtTracker;
        private readonly ILogger<GraphRepository> logger;
        private readonly IShellRepository shellRepository;
        private readonly IConceptDescriptionRepository conceptDescriptionRepository;
        private readonly IAdtClientUtil adtClientUtil;

        public GraphRepository(
            ILogger<GraphRepository> logger,
            IAdtTracker adtTracker,
            IAdtHandler adtHandler,
            IModelDataFlowSettings settings,
            IAdtRelationshipIdBuilder idBuilder,
            IShellRepository shellRepository,
            IConceptDescriptionRepository conceptDescriptionRepository,
            IAdtClientUtil adtClientUtil)
        : base(adtHandler, settings, logger, adtTracker, idBuilder)
        {
            this.adtTracker = adtTracker;
            this.logger = logger;
            this.shellRepository = shellRepository;
            this.conceptDescriptionRepository = conceptDescriptionRepository;
            this.adtClientUtil = adtClientUtil;
        }

        /// <summary>
        /// To create or replace the twins and relationships
        /// </summary>
        /// <param name="shells"></param>
        /// <returns></returns>
        public void CreateOrReplaceGraph(IEnumerable<Aas.Shell> shells, IEnumerable<Aas.ConceptDescription> conceptDescriptions)
        {
            foreach (var aas in shells)
            {
                this.shellRepository.CreateOrReplaceShell(aas);
            }

            foreach (var conceptDescription in conceptDescriptions)
            {
                this.conceptDescriptionRepository.CreateOrReplaceConceptDescription(conceptDescription);
            }
        }

        /// <summary>
        /// Removing the twins and relationships that are no longer needed and that have dt ids starting with aas_
        /// </summary>
        /// <returns></returns>
        public void RemoveUnwantedGraph()
        {
            this.RemoveUnwantedRelationships();

            this.RemoveUnwantedTwins();
        }

        /// <summary>
        /// Deletes twins that have dt ids starting with 'sm_' and also were not part of the model update request input
        /// </summary>
        /// <returns></returns>
        private void RemoveUnwantedTwins()
        {
            string twinsQuery = "SELECT T.$dtId FROM digitaltwins T WHERE STARTSWITH(T.$dtId, 'aas_')";
            try
            {
                var graphTwins = this.QueryTwins<BasicDigitalTwin>(twinsQuery);

                var graphTwinsList = graphTwins.ToHashSet();

                this.logger.GraphTotalTwins(graphTwinsList.Count);

                var adtTwinIds = this.adtTracker.GetTwinIds();

                this.logger.TotalTwinsRequestedInThisRun(adtTwinIds.Count());

                var twinIdsToBeRemovedList = graphTwinsList.Where(t => !adtTwinIds.Contains(t.Id)).ToList();

                this.logger.DeletingTwins(twinIdsToBeRemovedList.Count);

                foreach (var twin in twinIdsToBeRemovedList)
                {
                    this.DeleteTwin(twin.Id);
                }
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToQueryTwins(twinsQuery, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                throw;
            }
            finally
            {
                this.adtTracker.ClearTwinIds();
            }
        }

        /// <summary>
        /// Deletes relationships that have dt ids starting with 'aas_' and also were not part of the model update request input
        /// </summary>
        /// <returns></returns>
        private void RemoveUnwantedRelationships()
        {
            string relationshipsQuery = "SELECT R.$relationshipId, R.$sourceId FROM RELATIONSHIPS R WHERE STARTSWITH(R.$relationshipId, 'aas_')";
            try
            {
                var graphRelationships = this.QueryRelationships<BasicRelationship>(relationshipsQuery);

                var graphRelationshipsList = graphRelationships.ToHashSet();

                this.logger.GraphTotalRelationships(graphRelationshipsList.Count);

                var adtRelationshipIds = this.adtTracker.GetRelationshipIds();

                this.logger.TotalRelationshipsRequestedInThisRun(adtRelationshipIds.Count());

                var relationshipsToBeRemovedList = graphRelationshipsList.Where(w => !adtRelationshipIds.Contains(w.Id)).ToList();

                this.logger.DeletingRelationships(relationshipsToBeRemovedList.Count);

                foreach (var relationship in relationshipsToBeRemovedList)
                {
                    this.DeleteRelationship(relationship.SourceId, relationship.Id);
                }
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToQueryRelationships(relationshipsQuery, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                throw;
            }
            finally
            {
                this.adtTracker.ClearRelationshipIds();
            }
        }
    }
}
