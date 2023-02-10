using AasEnum = AasFactory.Azure.Models.Aas.Metamodels.Enums;
using FactoryEnum = AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.Interfaces;

/// <summary>
/// Converts various Aas Factory Enums to Aas enums.
/// </summary>
public interface IAasDataTypeConverter
{
    /// <summary>
    /// Converts a <see cref="FactoryEnum.DataType"/> enum to an <see cref="AasEnum.DataType"/>. <br/>
    /// </summary>
    /// <param name="dataType">Factory data type enum.</param>
    /// <exception cref="ArgumentException">Will throw exception if given an unknown or none type.</exception>
    /// <returns>aas data type enum.</returns>
    AasEnum.DataType ConvertFactoryDataTypeToAasDataType(FactoryEnum.DataType dataType);

    /// <summary>
    /// Converts a <see cref="FactoryEnum.DataType"/> enum to an <see cref="AasEnum.PropertyType"/>. <br/>
    /// </summary>
    /// <param name="dataType">Factory data type enum.</param>
    /// <exception cref="ArgumentException">Will throw exception if given an unknown or none type.</exception>
    /// <returns>aas property type enum.</returns>
    AasEnum.PropertyType ConvertFactoryDataTypeToAasPropertyType(FactoryEnum.DataType dataType);

    /// <summary>
    /// Converts a <see cref="FactoryEnum.StatType"/> enum to an <see cref="AasEnum.LevelType"/>. <br/>
    /// </summary>
    /// <param name="statType">Factory stat type enum.</param>
    /// <returns>aas level type enum.</returns>
    AasEnum.LevelType ConvertFactoryStatTypeToAasLevelType(FactoryEnum.StatType statType);

    /// <summary>
    /// Trying to convert a <see cref="FactoryEnum.DataType"/> enum to an <see cref="AasEnum.PropertyType"/>.
    /// </summary>
    /// <param name="dataType">AasFactory data type</param>
    /// <param name="propertyType">Aas property type</param>
    /// <returns></returns>
    bool TryConvertFactoryDataTypeToAasPropertyType(FactoryEnum.DataType dataType, out AasEnum.PropertyType propertyType);
}
