namespace AasFactory.Azure.Functions.Interfaces;

/// <summary>
/// General interface for any type of data conversion
/// </summary>
/// <typeparam name="TFrom">Type of the data to convert</typeparam>
/// <typeparam name="TTo">Converted data type</typeparam>
public interface IAdapter<TFrom, TTo>
{
    /// <summary>
    /// a method to convert from TFrom to TTo
    /// </summary>
    /// <param name="from">an input to convert</param>
    /// <returns>converted object</returns>
    TTo Convert(TFrom from);
}
