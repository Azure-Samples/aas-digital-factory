namespace AasFactory.Azure.Functions.StreamingDataFlow.Parameters;

public static class FactoryStreamingDataProcessorParameters
{
    // cache key prefixes
    /// <summary>
    /// The counter cache key.
    /// </summary>
    public const string CounterCacheKey = "counter";

    /// <summary>
    /// The last updated cache key (based on start time).
    /// </summary>
    public const string LastUpdatedCacheKey = "timestamp";

    /// <summary>
    /// The machine id field name.
    /// </summary>
    public const string MachineIdFieldName = "machine_id";
}