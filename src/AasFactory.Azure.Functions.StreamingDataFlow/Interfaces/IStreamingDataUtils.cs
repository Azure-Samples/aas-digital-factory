using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.EventHubs.Events.V1;
using AasFactory.Azure.Models.Factory.StreamingData;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;

public interface IStreamingDataUtils
{
    /// <summary>
    /// Extracts a streaming data field from the streaming Data.
    /// </summary>
    /// <param name="streamingData">The streaming Data.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The streaming data field if one exists else null.</returns>
    StreamingDataField? ExtractDataField(FactoryStreamingDataChanged streamingData, string propertyName);

    /// <summary>
    /// Validates the streaming data field based on the value type.
    /// </summary>
    /// <param name="dataField">The streaming data field.</param>
    /// <returns>A boolean that indicates if the data field is valid.</returns>
    bool IsValidDataFieldValue(StreamingDataField? dataField);

    /// <summary>
    ///  Returns the ADT AAS model id for the ADX streaming flow.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    string GetAdtModelIdForPropertyValueType(PropertyType type);

    /// <summary>
    /// Validates the streaming data payload.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    bool IsStreamingDataPayloadValid(FactoryStreamingDataChanged from);
}