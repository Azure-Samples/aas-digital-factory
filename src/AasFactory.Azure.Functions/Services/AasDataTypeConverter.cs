using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Functions.Logger;
using Microsoft.Extensions.Logging;
using AasEnum = AasFactory.Azure.Models.Aas.Metamodels.Enums;
using FactoryEnum = AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.Services;

/// <inheritdoc cref="IAasDataTypeConverter"/>
public class AasDataTypeConverter : IAasDataTypeConverter
{
    private readonly ILogger<AasDataTypeConverter> logger;

    public AasDataTypeConverter(ILogger<AasDataTypeConverter> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc />
    public AasEnum.DataType ConvertFactoryDataTypeToAasDataType(FactoryEnum.DataType dataType) =>
       dataType switch
       {
           FactoryEnum.DataType.BigInt => AasEnum.DataType.IntegerMeasure,
           FactoryEnum.DataType.Boolean => AasEnum.DataType.Boolean,
           FactoryEnum.DataType.DateTime => AasEnum.DataType.Date,
           FactoryEnum.DataType.Float32 => AasEnum.DataType.Rational,
           FactoryEnum.DataType.Float64 => AasEnum.DataType.Rational,
           FactoryEnum.DataType.Int => AasEnum.DataType.IntegerMeasure,
           FactoryEnum.DataType.String => AasEnum.DataType.String,
           var unknown => throw new ArgumentException($"The data type '{unknown}' has no conversion to {nameof(AasEnum.DataType)}."),
       };

    /// <inheritdoc />
    public AasEnum.PropertyType ConvertFactoryDataTypeToAasPropertyType(FactoryEnum.DataType dataType)
    {
        AasEnum.PropertyType returnType;

        if (this.TryConvertFactoryDataTypeToAasPropertyType(dataType, out returnType))
        {
            return returnType;
        }

        throw new ArgumentException($"The data type '{dataType}' has no conversion to {nameof(FactoryEnum.DataType)}");
    }

    /// <inheritdoc />
    public AasEnum.LevelType ConvertFactoryStatTypeToAasLevelType(FactoryEnum.StatType statType)
    {
        return statType switch
        {
            FactoryEnum.StatType.Continuous => AasEnum.LevelType.Number,
            _ => AasEnum.LevelType.None,
        };
    }

    /// <inheritdoc />
    public bool TryConvertFactoryDataTypeToAasPropertyType(FactoryEnum.DataType dataType, out AasEnum.PropertyType propertyType)
    {
        bool result = true;

        switch (dataType)
        {
            case FactoryEnum.DataType.BigInt:
                propertyType = AasEnum.PropertyType.Long;
                break;
            case FactoryEnum.DataType.Boolean:
                propertyType = AasEnum.PropertyType.Boolean;
                break;
            case FactoryEnum.DataType.DateTime:
                propertyType = AasEnum.PropertyType.DateTime;
                break;
            case FactoryEnum.DataType.Float32:
                propertyType = AasEnum.PropertyType.Float;
                break;
            case FactoryEnum.DataType.Float64:
                propertyType = AasEnum.PropertyType.Double;
                break;
            case FactoryEnum.DataType.Int:
                propertyType = AasEnum.PropertyType.Integer;
                break;
            case FactoryEnum.DataType.String:
                propertyType = AasEnum.PropertyType.String;
                break;
            default:
                propertyType = AasEnum.PropertyType.None;
                result = false;
                this.logger.UnableToConvertFactoryFieldTypeToAasPropertyType(dataType);
                break;
        }

        return result;
    }
}
