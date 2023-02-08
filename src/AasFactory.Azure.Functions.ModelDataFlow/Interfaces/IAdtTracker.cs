using AasFactory.Services;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces
{
    /// <summary>
    /// This implementation will track all the twins and relationships added with the model update flow request
    /// </summary>
    public interface IAdtTracker
    {
        /// <summary>
        /// Add twin id to the collection
        /// </summary>
        /// <param name="twinId"></param>
        void AddTwinId(string twinId);

        /// <summary>
        /// Return twin ids collection
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetTwinIds();

        /// <summary>
        /// Add relationship id to the collection
        /// </summary>
        /// <param name="relationshipId"></param>
        void AddRelationshipId(string relationshipId);

        /// <summary>
        /// Return relationship ids collection
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetRelationshipIds();

        /// <summary>
        /// Clear twin ids collection
        /// </summary>
        void ClearTwinIds();

        /// <summary>
        /// Clear relationship ids collection
        /// </summary>
        void ClearRelationshipIds();
    }
}