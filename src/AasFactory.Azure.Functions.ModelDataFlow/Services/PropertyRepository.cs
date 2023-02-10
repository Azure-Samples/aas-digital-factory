using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Functions.ModelDataFlow.Logger;
using AasFactory.Azure.Models.Adt.Twins;
using AasFactory.Services;
using Azure;
using Microsoft.Extensions.Logging;
using Aas = AasFactory.Azure.Models.Aas.Metamodels;
using AasEnums = AasFactory.Azure.Models.Aas.Metamodels.Enums;
using Adt = AasFactory.Azure.Models.Adt;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

public class PropertyRepository : BaseAdtRepository, IPropertyRepository
{
    private const string PropertyFailureMessage = "Failed to create a property because the property value type is not allowed.";
    private readonly IAdtTracker adtTracker;
    private readonly IModelDataFlowSettings settings;

    public PropertyRepository(
      ILogger<PropertyRepository> logger,
      IAdtTracker adtTracker,
      IAdtHandler adtHandler,
      IModelDataFlowSettings settings,
      IAdtRelationshipIdBuilder idBuilder)
        : base(adtHandler, settings, logger, adtTracker, idBuilder)
    {
        this.settings = settings;
        this.adtTracker = adtTracker;
    }

    /// <summary>
    /// This method handles creating the data type specific property
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public void CreateOrReplaceProperty(Aas.Property property)
    {
        Property? propertyTwin = this.CreateAdtPropertyFromAasProperty(property);
        this.adtTracker.AddTwinId(property.Id);
        if (propertyTwin is null)
        {
            this.Logger.InvalidPropertyType(property.Id, property.ValueType.ToString());
            if (this.settings.ContinueOnAdtErrors)
            {
                return;
            }
            else
            {
                throw new RequestFailedException(PropertyFailureMessage);
            }
        }

        this.CreateOrReplaceTwin(propertyTwin);
    }

    private Property? CreateAdtPropertyFromAasProperty(Aas.Property property) =>
        property.ValueType switch
        {
            AasEnums.PropertyType.Boolean => new Adt.Twins.BooleanProperty(property),
            AasEnums.PropertyType.Date => new Adt.Twins.DateProperty(property),
            AasEnums.PropertyType.DateTime => new Adt.Twins.DateTimeProperty(property),
            AasEnums.PropertyType.Double => new Adt.Twins.DoubleProperty(property),
            AasEnums.PropertyType.Duration => new Adt.Twins.DurationProperty(property),
            AasEnums.PropertyType.Float => new Adt.Twins.FloatProperty(property),
            AasEnums.PropertyType.Integer => new Adt.Twins.IntegerProperty(property),
            AasEnums.PropertyType.Long => new Adt.Twins.LongProperty(property),
            AasEnums.PropertyType.String => new Adt.Twins.StringProperty(property),
            AasEnums.PropertyType.Time => new Adt.Twins.TimeProperty(property),
            _ => null,
        };
}
