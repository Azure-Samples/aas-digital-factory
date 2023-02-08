using System.Diagnostics.CodeAnalysis;
using AasFactory.Services.Utils;
using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public class ModelDataFlowSettings : IModelDataFlowSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelDataFlowSettings"/> class.
    /// </summary>
    /// <param name="config">A configuration.</param>
    public ModelDataFlowSettings(IConfiguration config)
    {
        this.AasEventHubName = config.GetValue<string>("AAS_EVENT_HUB_NAME");
        this.AbbreviatedCompanyName = config.GetValue<string>("ABBREVIATED_COMPANY_NAME");
        this.FactoryEventHubName = config.GetValue<string>("FACTORY_EVENT_HUB_NAME");
        this.StorageAccountContainerName = config.GetValue<string>("STORAGE_ACCOUNT_CONTAINER_NAME");
        this.StorageAccountConnectionString = config.GetValue<string>("STORAGE_ACCOUNT_CONNECTION_STRING");
        this.DigitalTwinsInstanceUrl = config.GetValue<string>("ADT_INSTANCE_URL");
        this.ShellsStoragePath = config.GetValue<string>("SHELLS_STORAGE_PATH");
        this.ContinueOnAdtErrors = config.GetValue<bool>("CONTINUE_ON_ADT_ERRORS", true);
        this.SimmyDependencyFaultTolerance = config.GetValue<string>("SIMMY_DEPENDENCY_FAULT_TOLERANCE", string.Empty);
        this.SimmyInjectionRate = config.GetValue<double>("SIMMY_INJECTION_RATE", 0);

        // Validation
        Guard.ThrowIfNull("AAS_EVENT_HUB_NAME", this.AasEventHubName);
        Guard.ThrowIfNull("ABBREVIATED_COMPANY_NAME", this.AbbreviatedCompanyName);
        Guard.ThrowIfNull("FACTORY_EVENT_HUB_NAME", this.FactoryEventHubName);
        Guard.ThrowIfNull("STORAGE_ACCOUNT_CONTAINER_NAME", this.StorageAccountContainerName);
        Guard.ThrowIfNull("STORAGE_ACCOUNT_CONNECTION_STRING", this.StorageAccountConnectionString);
        Guard.ThrowIfNull("ADT_INSTANCE_URL", this.DigitalTwinsInstanceUrl);
        Guard.ThrowIfNull("SHELLS_STORAGE_PATH", this.ShellsStoragePath);
    }

    /// <inheritdoc />
    public string AasEventHubName { get; private set; }

    /// <inheritdoc />
    public string AbbreviatedCompanyName { get; private set; }

    /// <inheritdoc />
    public string FactoryEventHubName { get; private set; }

    /// <inheritdoc />
    public string StorageAccountContainerName { get; private set; }

    /// <inheritdoc />
    public string StorageAccountConnectionString { get; private set; }

    /// <inheritdoc />
    public string DigitalTwinsInstanceUrl { get; private set; }

    /// <inheritdoc />
    public string ShellsStoragePath { get; private set; }

    /// <inheritdoc />
    public string SimmyDependencyFaultTolerance { get; }

    /// <inheritdoc />
    public double SimmyInjectionRate { get; }

    /// <inheritdoc />
    public bool ContinueOnAdtErrors { get; private set; }
}