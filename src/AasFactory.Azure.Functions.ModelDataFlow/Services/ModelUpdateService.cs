using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Models.Aas.Metamodels;
using Microsoft.Extensions.Logging;
using Aas = AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services
{
    /// <summary>
    /// This service will be used for the Model update flow operations
    /// </summary>
    public class ModelUpdateService : IModelUpdateService
    {
        private readonly ILogger<IModelUpdateService> logger;
        private readonly IGraphRepository graphRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="twinrepository"></param>
        /// <param name="relationshipsRepository"></param>
        /// <param name="logger"></param>
        public ModelUpdateService(IModelDataFlowSettings settings, ILoggerFactory loggerFactory, IGraphRepository graphRepository)
        {
            this.logger = loggerFactory.CreateLogger<ModelUpdateService>();
            this.graphRepository = graphRepository;
        }

        /// <summary>
        /// Creates a connected graph for AAS metamodels. In addition, it removes the twins and relationships not needed in the graph graph.
        /// </summary>
        /// <param name="shells"></param>
        /// <param name="conceptDescriptions"></param>
        /// <returns></returns>
        public void BuildGraph(IEnumerable<Aas.Shell> shells, IEnumerable<ConceptDescription> conceptDescriptions)
        {
            try
            {
                this.logger.CreatingOrReplacingTwinsAndRelationships();

                this.graphRepository.CreateOrReplaceGraph(shells, conceptDescriptions);

                this.logger.DoneCreatingOrReplacingTwinsAndRelationships();

                // Handle unwanted twins and relationships.
                this.logger.DeletingTwinsAndRelationships();

                this.graphRepository.RemoveUnwantedGraph();

                this.logger.DoneDeletingTwinsAndRelationships();
            }
            catch (Exception ex)
            {
                this.logger.FailedToBuildgraph(ex);
                throw;
            }
        }
    }
}