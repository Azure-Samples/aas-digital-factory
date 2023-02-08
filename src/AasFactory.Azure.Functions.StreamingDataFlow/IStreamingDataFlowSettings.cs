/// <summary>
/// The configuration used for Model Data Flow function.
/// </summary>
public interface IStreamingDataFlowSettings
{
    /// <summary>
    /// The name of the AAS Event Hub.
    /// </summary>
    string AasEventHubName { get; }

    /// <summary>
    /// The name of the Factory Event Hub.
    /// </summary>
    string FactoryEventHubName { get; }

    /// <summary>
    /// The URL for the Azure Digital Twins instance.
    /// </summary>
    string DigitalTwinsInstanceUrl { get; }

    /// <summary>
    /// The number of consecutive exceptions allowed before the circuit breaks for ADT SDK calls.
    /// </summary>
    /// <value></value>
    int CircuitBreakerAllowedExceptionCount { get; }

    /// <summary>
    /// The duration of the circuit break after the number of max allowed exceptions is reached.
    /// </summary>
    /// <value></value>
    int CircuitBreakerWaitTimeSec { get; }

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
}