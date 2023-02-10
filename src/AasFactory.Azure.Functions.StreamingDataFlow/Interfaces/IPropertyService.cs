using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.StreamingDataFlow.Interfaces
{
    public interface IPropertyService
    {
        /// <summary>
        /// Given the new telemetry value for a property to update, this method updates the
        /// (1) "value" field, (2) "{type}Value" field, and (3) sourceTime metadata on
        /// the specified twin.
        /// </summary>
        /// <param name="propertyField">Object representing the property and value to update.</param>
        /// <param name="cycleStartTime">The start time of the cycle - there will be a StartTime event
        /// associated with each set of telemetry events that is processed.</param>
        /// <returns></returns>
        Task UpdatePropertyValues(PropertyField propertyField, string cycleStartTime);
    }
}