using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Functions.EventHandler;
using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.EventHandler;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;
using AasFactory.Azure.Functions.ModelDataFlow.Services;
using AasFactory.Azure.Functions.ModelDataFlow.Services.AasConverters;
using AasFactory.Azure.Functions.Services;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;
using AasFactory.Services;
using AasFactory.Services.Utils;
using Azure.Identity;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AasFactory.Azure.Functions.ModelDataFlow.Startup))]

namespace AasFactory.Azure.Functions.ModelDataFlow;

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
        // creating blob service client
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

        // config
        var config = this.GetConfiguration(builder);
        var settings = new ModelDataFlowSettings(config);
        builder.Services.AddSingleton<IModelDataFlowSettings>(settings);

        // azure services/handlers
        builder.Services.AddSingleton<IAdtHandler, AdtHandler>();
        builder.Services.AddSingleton<IBlobStorageHandler, BlobStorageHandler>();
        builder.Services.AddSingleton<IAdtClientUtil, AdtClientUtil>();

        // scoped objects
        builder.Services.AddScoped<IAdtTracker, AdtTracker>();

        // repositories
        builder.Services.AddScoped<IGraphRepository, GraphRepository>();
        builder.Services.AddScoped<IShellRepository, ShellRepository>();
        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<IConceptDescriptionRepository, ConceptDescriptionRepository>();

        // services
        builder.Services.AddScoped<IModelUpdateService, ModelUpdateService>();

        // Add Polly
        builder.Services.AddPollyPolicies(settings.SimmyInjectionRate, settings.SimmyDependencyFaultTolerance);

        // integration event handlers
        builder.Services.AddTransient<IIntegrationEventHandlerWithReturn<FactoryModelDataEventChanged, EventData>, FactoryModelDataEventChangedHandler>();
        builder.Services.AddTransient<IIntegrationEventHandler<AasModelDataEventChanged>, AasModelDataEventChangedHandler>();

        // conversion services
        builder.Services.AddSingleton<IConverterHelpers, ConverterHelpers>();
        builder.Services.AddSingleton<IAdapter<Line, Shell>, LineConverter>();
        builder.Services.AddSingleton<IAdapter<Factory, Shell>, FactoryConverter>();
        builder.Services.AddSingleton<IAdapter<MachineType, Shell>, MachineTypeConverter>();
        builder.Services.AddSingleton<IAdapter<(Machine machine, MachineType machineType), Shell>, MachineConverter>();
        builder.Services.AddSingleton<IAdapter<(MachineTypeField field, string machineTypeId, SubModelType subModelType), ConceptDescription>, ConceptDescriptionConverter>();
        builder.Services.AddSingleton<IAasIdBuilder, AasIdBuilder>();
        builder.Services.AddSingleton<IAdtRelationshipIdBuilder, AdtRelationshipIdBuilder>();
        builder.Services.AddSingleton<IAasDataTypeConverter, AasDataTypeConverter>();
        builder.Services.AddSingleton<IAasConverter, AasConverter>();
        builder.Services.AddSingleton<IAasShellBuilder, AasShellBuilder>();
    }

    public virtual IConfiguration GetConfiguration(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();
        return context.Configuration;
    }
}