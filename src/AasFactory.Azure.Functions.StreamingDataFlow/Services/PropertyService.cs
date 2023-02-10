using AasFactory.Azure.Functions.StreamingDataFlow.Interfaces;
using AasFactory.Azure.Functions.StreamingDataFlow.Logger;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Services;
using Azure;
using Microsoft.Extensions.Logging;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Services;

public class PropertyService : IPropertyService
{
    private readonly IAasToAdtDataTypeConverter aasToAdtDataTypeConverter;
    private readonly IAdtClient sdkClient;

    public PropertyService(
      ILogger<PropertyService> logger,
      IAasToAdtDataTypeConverter aasToAdtDataTypeConverter,
      IAdtHandler adtHandler,
      IStreamingDataFlowSettings settings)
    {
        this.aasToAdtDataTypeConverter = aasToAdtDataTypeConverter;
        this.sdkClient = adtHandler.GetAdtClient(settings.DigitalTwinsInstanceUrl, true);
        this.Logger = logger;
    }

    protected ILogger Logger { get; private set; }

    /// <inheritdoc/>
    public async Task UpdatePropertyValues(PropertyField propertyField, string timestamp)
    {
        // Get twin ID to update
        var twinId = propertyField.Id;
        if (string.IsNullOrWhiteSpace(twinId))
        {
          this.Logger.PropertyMissingRequiredField(propertyField.ToString()!, "Id");
          return;
        }

        // Parse data value based on property value type
        KeyValuePair<string, object> adtValueWithValueKey;
        try
        {
          adtValueWithValueKey = this.aasToAdtDataTypeConverter.ParseValueBasedOnDataTypeAndGetValueKey(propertyField.ValueType!, propertyField.Value!);
        }
        catch (ArgumentException)
        {
          this.Logger.PropertyUpdateFailedInvalidPropertyType(twinId, propertyField.ValueType.ToString());
          return;
        }

        // Add property twin value update to patch
        var cycleStartTimeDateTime = DateTime.Parse(timestamp);
        var patchDocument = new JsonPatchDocument();

        var valueKeyPath = "/value";
        patchDocument.AppendAdd(valueKeyPath, propertyField.Value);
        this.Logger.UpdatingFieldOnPropertyWithNewValue(valueKeyPath, twinId, propertyField.Value);

        var valueMetadataPath = "/$metadata/value/sourceTime";
        patchDocument.AppendAdd(valueMetadataPath, cycleStartTimeDateTime);
        this.Logger.UpdatingFieldOnPropertyWithNewValue(valueMetadataPath, twinId, timestamp);

        if (propertyField.ValueType != PropertyType.String)
        {
          var typedValueKeyPath = $"/{adtValueWithValueKey.Key}";
          patchDocument.AppendAdd(typedValueKeyPath, adtValueWithValueKey.Value);
          this.Logger.UpdatingFieldOnPropertyWithNewValue(typedValueKeyPath, twinId, adtValueWithValueKey.Value.ToString()!);

          // Add property metadata with cycle start time as sourceTime to patch
          var metadataPath = $"/$metadata/{adtValueWithValueKey.Key}/sourceTime";
          patchDocument.AppendAdd(metadataPath, cycleStartTimeDateTime);
          this.Logger.UpdatingFieldOnPropertyWithNewValue(metadataPath, twinId, timestamp);
        }

        // Call SDK to send patch document to ADT
        await this.sdkClient.UpdateTwinAsync(twinId, patchDocument);
    }
}
