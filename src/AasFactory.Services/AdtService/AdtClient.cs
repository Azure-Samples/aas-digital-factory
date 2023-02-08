using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using AasFactory.Services.Logging;
using AasFactory.Services.Utils;
using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace AasFactory.Services
{
    [ExcludeFromCodeCoverage]
    public class AdtClient : IAdtClient
    {
        private readonly DigitalTwinsClient client;
        private readonly IAsyncPolicy policyAsync;
        private readonly Policy policy;
        private readonly ILogger logger;
        private readonly IAdtClientUtil adtClientUtil;
        private readonly bool continueWithAdtErrors;

        public AdtClient(
          DigitalTwinsClient client,
          ILoggerFactory loggerFactory,
          IReadOnlyPolicyRegistry<string> policyRegistry,
          IAdtClientUtil adtClientUtil,
          bool continueWithAdtErrors)
        {
            this.client = client;
            this.logger = loggerFactory.CreateLogger<AdtClient>();
            this.policy = policyRegistry.Get<Policy>(PolicyExtensions.AdtPolicyName);
            this.policyAsync = policyRegistry.Get<IAsyncPolicy>(PolicyExtensions.AdtAsyncPolicyName);
            this.adtClientUtil = adtClientUtil;
            this.continueWithAdtErrors = continueWithAdtErrors;
        }

        /// <summary>
        /// Creates/Replaces the twin specified.
        /// </summary>
        /// <param name="twinId">ID of the twin to create or replace</param>
        /// <param name="twin">Custom AAS twin</param>
        public void CreateOrReplaceTwin<T>(string twinId, T twin)
        where T : BasicDigitalTwin
        {
            try
            {
                this.logger.CreatingTwin(twinId);

                this.policy!.Execute<T>(() =>
                    this.client.CreateOrReplaceDigitalTwin<T>(twinId, twin));

                this.logger.SuccessfullyCreatedTwin(twin.GetType().Name, twin.Id);
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToCreateTwin(twinId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                if (this.adtClientUtil.ShouldNotContinueForCreateOrReplace(e.Status, this.continueWithAdtErrors))
                {
                  throw;
                }
            }
        }

        /// <summary>
        /// Deletes the twin specified.
        /// </summary>
        /// <param name="twinId">ID of the twin to create or replace</param>
        public void DeleteTwin(string twinId)
        {
            try
            {
                this.policy!.Execute(() =>
                    this.client.DeleteDigitalTwin(twinId));
                this.logger.SuccessfullyDeletedTwin(twinId);
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToDeleteTwin(twinId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                if (this.adtClientUtil.ShouldNotContinueForDelete(e.Status, this.continueWithAdtErrors))
                {
                  throw;
                }
            }
        }

        /// <summary>
        /// Updates an existing twin
        /// </summary>
        /// <param name="twinId">ID of the twin to update</param>
        /// <param name="patchDocument">Description of changes to make to the twin</param>
        public async Task UpdateTwinAsync(string twinId, JsonPatchDocument patchDocument)
        {
            try
            {
                await this.policyAsync!.ExecuteAsync(async () =>
                  await this.client.UpdateDigitalTwinAsync(twinId, patchDocument).ConfigureAwait(false))
                  .ConfigureAwait(false);
            }
            catch (RequestFailedException e)
            {
              // This is mainly used by streaming data flow and error details should be logged and exeuction should always continue.
              this.logger.FailedToUpdateTwin(twinId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
            }
        }

        /// <summary>
        /// deletes the relationship.
        /// </summary>
        /// <param name="twinId">ID of the parent twin</param>
        /// <param name="relationshipId">relationship id</param>
        public void DeleteRelationship(string twinId, string relationshipId)
        {
            try
            {
                this.policy.Execute(() =>
                    this.client.DeleteRelationship(twinId, relationshipId));
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToDeleteRelationship(twinId, relationshipId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                if (this.adtClientUtil.ShouldNotContinueForDelete(e.Status, this.continueWithAdtErrors))
                {
                  throw;
                }
            }
        }

        /// <summary>
        /// creates/replaces the relationship.
        /// </summary>
        /// <param name="twinId">ID of the parent twin</param>
        /// <param name="relationshipId">relationship id</param>
        /// <param name="relationship">custom relationship definition</param>
        public void CreateOrReplaceRelationship<TRel>(string twinId, string relationshipId, TRel relationship)
        where TRel : BasicRelationship
        {
            try
            {
                this.policy.Execute(() =>
                    this.client.CreateOrReplaceRelationship(twinId, relationshipId, relationship));
                this.logger.SuccessfullyCreatedRelationship(relationship.Name, relationship.SourceId, relationship.TargetId);
            }
            catch (RequestFailedException e)
            {
                this.logger.FailedToCreateRelationship(twinId, relationshipId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
                if (this.adtClientUtil.ShouldNotContinueForCreateOrReplace(e.Status, this.continueWithAdtErrors))
                {
                  throw;
                }
            }
        }

        /// <summary>
        /// updates the relationship.
        /// </summary>
        /// <param name="twinId">ID of the parent twin</param>
        /// <param name="relationshipId">relationship id</param>
        /// <param name="patchDocument">patch document with the update details</param>
        public void UpdateRelationship(string twinId, string relationshipId, JsonPatchDocument patchDocument)
        {
            try
            {
                this.policy.Execute(() =>
                    this.client.UpdateRelationship(twinId, relationshipId, patchDocument));
            }
            catch (RequestFailedException e)
            {
              this.logger.FailedToUpdateRelationship(twinId, relationshipId, e.ErrorCode!, e.Status.ToString(), this.adtClientUtil.RequestFailedExceptionFriendlyMessage(e.Message));
            }
        }

        /// <summary>
        /// Returns the twins from the ADT instance based on the query string
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Pageable<T> QueryTwins<T>(string query)
        where T : BasicDigitalTwin
        {
            return this.client.Query<T>(query);
        }

        /// <summary>
        /// Returns the relationships from the ADT instance based on the query string
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Pageable<TRel> QueryRelationships<TRel>(string query)
        where TRel : BasicRelationship
        {
            return this.client.Query<TRel>(query);
        }
    }
}