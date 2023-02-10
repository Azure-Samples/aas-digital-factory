using System.Diagnostics.CodeAnalysis;
using AasFactory.Services.Utils;
using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public class StreamingDataFlowSettings : IStreamingDataFlowSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamingDataFlowSettings"/> class.
    /// </summary>
    /// <param name="config">A configuration.</param>
    public StreamingDataFlowSettings(IConfiguration config)
    {
        this.AasEventHubName = config.GetValue<string>("AAS_EVENT_HUB_NAME");
        this.FactoryEventHubName = config.GetValue<string>("FACTORY_EVENT_HUB_NAME");
        this.DigitalTwinsInstanceUrl = config.GetValue<string>("ADT_INSTANCE_URL");
        this.CircuitBreakerAllowedExceptionCount = config.GetValue<int>("CIRCUIT_BREAKER_ALLOWED_EXCEPTION_COUNT", 3);
        this.CircuitBreakerWaitTimeSec = config.GetValue<int>("CIRCUIT_BREAKER_WAIT_TIME_SEC", 60);
        this.SimmyDependencyFaultTolerance = config.GetValue<string>("SIMMY_DEPENDENCY_FAULT_TOLERANCE", string.Empty);
        this.SimmyInjectionRate = config.GetValue<double>("SIMMY_INJECTION_RATE", 0);

        Guard.ThrowIfNull("AAS_EVENT_HUB_NAME", this.AasEventHubName);
        Guard.ThrowIfNull("FACTORY_EVENT_HUB_NAME", this.FactoryEventHubName);
        Guard.ThrowIfNull("ADT_INSTANCE_URL", this.DigitalTwinsInstanceUrl);
    }

    /// <inheritdoc />
    public string AasEventHubName { get; private set; }

    /// <inheritdoc />
    public string FactoryEventHubName { get; private set; }

    /// <inheritdoc />
    public string DigitalTwinsInstanceUrl { get; private set; }

    /// <inheritdoc />
    public int CircuitBreakerAllowedExceptionCount { get; }

    /// <inheritdoc />
    public int CircuitBreakerWaitTimeSec { get; }

    /// <inheritdoc />
    public string SimmyDependencyFaultTolerance { get; }

    /// <inheritdoc />
    public double SimmyInjectionRate { get; }
}