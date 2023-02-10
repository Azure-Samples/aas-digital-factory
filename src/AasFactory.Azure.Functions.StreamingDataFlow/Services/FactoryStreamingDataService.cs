using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Logger;
using AasFactory.Azure.Models.EventHubs;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory.Enums;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Services;

public class FactoryStreamingDataService : IFactoryStreamingDataService
{
    private readonly ILogger<FactoryStreamingDataService> logger;
    private readonly IAdapter<FactoryStreamingDataChanged, AasStreamingDataChanged> streamingDataToAasConverter;
    private readonly IStreamingDataUtils streamingDataUtils;

    public FactoryStreamingDataService(
        IAdapter<FactoryStreamingDataChanged, AasStreamingDataChanged> streamingDataToAasConverter,
        IStreamingDataUtils streamingDataUtils,
        ILogger<FactoryStreamingDataService> logger)
    {
        this.streamingDataToAasConverter = streamingDataToAasConverter;
        this.streamingDataUtils = streamingDataUtils;
        this.logger = logger;
    }

    /// <inheritdoc />
    public IntegrationEvent? ProcessStreamingData(FactoryStreamingDataChanged streamingData)
    {
        IntegrationEvent? aasStreamingDataChanged = null!;
        if (!this.streamingDataUtils.IsStreamingDataPayloadValid(streamingData))
        {
            this.logger.FactoryStreamingDataIsNotValid();
            return aasStreamingDataChanged;
        }

        switch (streamingData.Header.ModelType)
        {
            case ModelInstanceType.MachineType:
                aasStreamingDataChanged = this.streamingDataToAasConverter.Convert(streamingData);
                break;
            default:
                break;
        }

        return aasStreamingDataChanged;
    }
}
