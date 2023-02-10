using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.Services;
using AasFactory.Azure.Functions.StreamingDataFlow.Converters;
using AasFactory.Azure.Functions.StreamingDataFlow.EventHandler;
using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Services;
using AasFactory.Azure.Functions.StreamingDataFlow.Utils;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory.Enums;
using AasFactory.Services;
using AasFactory.Services.Utils;
using Azure.Identity;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AasFactory.Azure.Functions.StreamingDataFlow.Startup))]

namespace AasFactory.Azure.Functions.StreamingDataFlow;

/// <summary>
/// This startup class allows for dependency injection.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    /// <summary>
    /// This method is called during startup to allow for the registration of services.
    /// </summary>
    /// <param name="builder">The builder that contains the service collection.</param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        DefaultAzureCredential credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ExcludeEnvironmentCredential = true,
            ExcludeManagedIdentityCredential = false,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCredential = true,
            ExcludeVisualStudioCodeCredential = true,
#if DEBUG
            ExcludeAzureCliCredential = false,
#else
            ExcludeAzureCliCredential = true,
#endif
            ExcludeInteractiveBrowserCredential = true,
        });
        builder.Services.AddSingleton<DefaultAzureCredential>(credential);

        var config = this.GetConfiguration(builder);

        // config
        var settings = new StreamingDataFlowSettings(config);
        builder.Services.AddSingleton<IStreamingDataFlowSettings>(settings);

        // Add Polly
        builder.Services.AddPollyPolicies(settings.CircuitBreakerAllowedExceptionCount, settings.CircuitBreakerWaitTimeSec, settings.SimmyInjectionRate, settings.SimmyDependencyFaultTolerance);

        // Azure client handlers
        builder.Services.AddSingleton<IAdtHandler, AdtHandler>();
        builder.Services.AddSingleton<IAdtClientUtil, AdtClientUtil>();
        builder.Services.AddSingleton<IBlobStorageHandler, BlobStorageHandler>();

        // utils
        builder.Services.AddSingleton<IStreamingDataUtils, StreamingDataUtils>();

        // singleton registration
        builder.Services.AddSingleton<IAasToAdtDataTypeConverter, AasToAdtDataTypeConverter>();
        builder.Services.AddSingleton<IAdapter<FactoryStreamingDataChanged, AasStreamingDataChanged>, FactoryStreamingDataToAasConverter>();
        builder.Services.AddSingleton<IAasIdBuilder, AasIdBuilder>();
        builder.Services.AddSingleton<IAasDataTypeConverter, AasDataTypeConverter>();
        builder.Services.AddSingleton<IAdapter<(string, ModelInstanceType), SubModelType>, FieldNameToSubmodelTypeConverter>();
        builder.Services.AddSingleton<IAdapter<ModelInstanceType, string?>, ModelInstanceTypeToSourceTimestampFieldNameConverter>();

        // repositories
        builder.Services.AddScoped<IPropertyService, PropertyService>();

        // scoped
        builder.Services.AddScoped<IIntegrationEventHandlerAsync<AasStreamingDataChanged>, AasStreamingDataChangedHandlerAsync>();

        builder.Services.AddScoped<IIntegrationEventHandlerWithReturn<FactoryStreamingDataChanged, EventData?>, FactoryStreamingDataChangedHandler>();
        builder.Services.AddScoped<IFactoryStreamingDataService, FactoryStreamingDataService>();
    }

    public virtual IConfiguration GetConfiguration(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();
        return context.Configuration;
    }
}