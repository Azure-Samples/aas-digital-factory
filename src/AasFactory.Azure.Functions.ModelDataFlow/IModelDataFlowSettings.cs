/// <summary>
/// The configuration used for Model Data Flow function.
/// </summary>
public interface IModelDataFlowSettings
{
    /// <summary>
    /// The name of the AAS Event Hub. This represents the AAS_EVENT_HUB_NAME environment variable.
    /// </summary>
    string AasEventHubName { get; }

    /// <summary>
    /// The abbreviated name of the company the instance is deployed in. This represents the ABBREVIATED_COMPANY_NAME environment variable.
    /// </summary>
    string AbbreviatedCompanyName { get; }

    /// <summary>
    /// The name of the Factory Event Hub. This represents the FACTORY_EVENT_HUB_NAME environment variable.
    /// </summary>
    string FactoryEventHubName { get; }

    /// <summary>
    /// The container name of the Storage Account. This represents the STORAGE_ACCOUNT_CONTAINER_NAME environment variable.
    /// </summary>
    string StorageAccountContainerName { get; }

    /// <summary>
    /// The connection string for the Storage Account. This represents the STORAGE_ACCOUNT_CONNECTION_STRING environment variable.
    /// </summary>
    string StorageAccountConnectionString { get; }

    /// <summary>
    /// The URL for the Azure Digital Twins instance. This represents the ADT_INSTANCE_URL environment variable.
    /// </summary>
    string DigitalTwinsInstanceUrl { get; }

    /// <summary>
    /// Path to directory in blob to store Shells.
    /// </summary>
    string ShellsStoragePath { get; }

    /// <summary>
    /// The dependency name to perform chaos testing (Simmy). If value is empty, chaos testing policies won't be enabled.
    /// Possible values: "Adt", "Redis", "Storage. // TODO: make enum?
    /// </summary>
    /// <value></value>
    string SimmyDependencyFaultTolerance { get; }

    /// <summary>
    /// The injection rate to perform chaos testing (Simmy). If 'SimmyDependencyFaultTolerance' property is empty, chaos testing policies won't be enabled.
    /// </summary>
    /// <value></value>
    double SimmyInjectionRate { get; }

    /// <summary>
    /// Flag to fail/proceed when there is single RequestFailedException for Adt client calls
    /// </summary>
    /// <value></value>
    bool ContinueOnAdtErrors { get; }
}