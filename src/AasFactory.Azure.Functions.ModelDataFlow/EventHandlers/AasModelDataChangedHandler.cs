using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Models.Aas;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AasFactory.Azure.Functions.ModelDataFlow.EventHandler;

/// <summary>
/// The processor for incoming payloads from the AAS conversion function to import into ADT.
/// </summary>
public class AasModelDataEventChangedHandler : IIntegrationEventHandler<AasModelDataEventChanged>
{
    private readonly IModelDataFlowSettings settings;
    private readonly IBlobStorageHandler blobStorageHandler;
    private readonly ILogger log;
    private readonly IModelUpdateService modelUpdateService;

    public AasModelDataEventChangedHandler(
      IModelDataFlowSettings settings,
      IBlobStorageHandler blobStorageHandler,
      ILoggerFactory loggerFactory,
      IModelUpdateService modelUpdateService)
    {
      this.settings = settings;
      this.blobStorageHandler = blobStorageHandler;
      this.log = loggerFactory.CreateLogger<AasModelDataEventChangedHandler>();
      this.modelUpdateService = modelUpdateService;
    }

    public void Handle(AasModelDataEventChanged eventData)
    {
        IBlobServiceClient blobServiceClient =
            this.blobStorageHandler.GetBlobServiceClient(this.settings.StorageAccountConnectionString);

        IBlobClient blobClient = blobServiceClient.GetBlobClient(this.settings.StorageAccountContainerName, eventData.Path);
        BlobContent blobContent = blobClient.Download();

        var aasShells = JsonConvert.DeserializeObject<AasShells>(blobContent.Content)!;
        var flatShells = this.FlattenShells(aasShells);
        this.log.DownloadedAasContent(blobContent.Content);

        this.modelUpdateService.BuildGraph(flatShells, aasShells.ConceptDescriptions);
        this.log.ProcessedAasToAdt();
    }

    private IEnumerable<Shell> FlattenShells(AasShells aasShells) =>
        new IEnumerable<Shell>[]
        {
            aasShells.Factories,
            aasShells.Lines,
            aasShells.MachineTypes,
            aasShells.Machines,
        }
        .SelectMany(shell => shell)
        .ToArray();
}