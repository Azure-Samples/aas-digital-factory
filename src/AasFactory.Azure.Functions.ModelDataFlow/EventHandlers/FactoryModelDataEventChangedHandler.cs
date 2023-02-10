using System.Diagnostics;
using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.EventHubs.Extensions;
using AasFactory.Azure.Models.Factory.Request;
using AasFactory.Services;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AasFactory.Azure.Functions.ModelDataFlow.EventHandler;

/// <summary>
/// The processor converts the incoming payload to AAS
/// </summary>
public class FactoryModelDataEventChangedHandler : IIntegrationEventHandlerWithReturn<FactoryModelDataEventChanged, EventData>
{
    private readonly IAasShellBuilder aasShellBuilder;
    private readonly IBlobStorageHandler blobStorageHandler;
    private readonly IModelDataFlowSettings modelDataFlowSettings;
    private readonly ILogger<FactoryModelDataEventChangedHandler> logger;

    public FactoryModelDataEventChangedHandler(
        IAasShellBuilder aasShellBuilder,
        IBlobStorageHandler blobStorageHandler,
        IModelDataFlowSettings modelDataFlowSettings,
        ILogger<FactoryModelDataEventChangedHandler> logger)
    {
        this.aasShellBuilder = aasShellBuilder;
        this.blobStorageHandler = blobStorageHandler;
        this.modelDataFlowSettings = modelDataFlowSettings;
        this.logger = logger;
    }

    public EventData Handle(FactoryModelDataEventChanged eventData)
    {
        // getting the blob client
        var blobServiceClient = this.blobStorageHandler.GetBlobServiceClient(this.modelDataFlowSettings.StorageAccountConnectionString);
        var downloadBlobClient = blobServiceClient.GetBlobClient(this.modelDataFlowSettings.StorageAccountContainerName, eventData.Path);

        // deserialize to the blob
        this.logger.DownloadingFactoryData(eventData.Path);
        var watch = Stopwatch.StartNew();
        watch.Start();
        var payload = this.TryDownloadBlobAndDeserialize(downloadBlobClient);
        watch.Stop();
        this.logger.SuccessfullyDownloadedFactoryData(eventData.Path, watch.Elapsed);

        // converting and serialize the AAS Models and save in blob storage
        this.logger.ConvertingToAasModel();
        watch.Reset();
        watch.Start();
        var aasShellStringified = this.TryBuildAasShellsAndSerialize(payload);
        var aasBlobPath = $"{this.modelDataFlowSettings.ShellsStoragePath}/{eventData.OutputFileName}.json";
        var uploadBlobClient = blobServiceClient.GetBlobClient(this.modelDataFlowSettings.StorageAccountContainerName, aasBlobPath);

        try
        {
            uploadBlobClient.Upload(aasShellStringified, overwrite: true);
        }
        catch (Exception ex)
        {
            this.logger.FailedToUploadAas(ex, aasBlobPath);
            throw;
        }

        watch.Stop();
        this.logger.SuccessfullyConvertedFactoryData(watch.ElapsedMilliseconds);

        // creating event and returning
        var eventBody = new ModelDataEventChanged()
        {
            Path = aasBlobPath,
        };

        var eventBodyStringified = JsonConvert.SerializeObject(eventBody);
        var returnedEventData = new EventData(eventBodyStringified);

        var outputEventType = typeof(AasModelDataEventChanged);
        returnedEventData.SetEventType(outputEventType.FullName!);

        return returnedEventData;
    }

    private ModelDataRequest TryDownloadBlobAndDeserialize(IBlobClient blobClient)
    {
        try
        {
            var blobContent = blobClient.Download();
            return JsonConvert.DeserializeObject<ModelDataRequest>(blobContent.Content)!;
        }
        catch (Exception ex)
        {
            this.logger.FailedToReadBlob(ex);
            throw;
        }
    }

    private string TryBuildAasShellsAndSerialize(ModelDataRequest modelDataRequest)
    {
        try
        {
            var aasShells = this.aasShellBuilder.BuildAasShells(
                modelDataRequest.MachineType,
                modelDataRequest.Factory,
                modelDataRequest.Line,
                modelDataRequest.Machine);
            var aasShellStringified = JsonConvert.SerializeObject(aasShells);

            this.logger.BuiltAasShells(
                aasShells.Factories.Count(),
                aasShells.MachineTypes.Count(),
                aasShells.Lines.Count(),
                aasShells.Machines.Count(),
                aasShells.ConceptDescriptions.Count());

            return aasShellStringified;
        }
        catch (Exception ex)
        {
            this.logger.FailedToBuildShells(ex);
            throw;
        }
    }
}