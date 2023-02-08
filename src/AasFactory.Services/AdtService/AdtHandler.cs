using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Polly.Registry;

namespace AasFactory.Services
{
    [ExcludeFromCodeCoverage]
    public class AdtHandler : IAdtHandler
    {
        private readonly DefaultAzureCredential credential;
        private readonly ILoggerFactory loggerFactory;
        private readonly IReadOnlyPolicyRegistry<string> policyRegistry;
        private readonly IAdtClientUtil adtClientUtil;

        private readonly ConcurrentDictionary<string, IAdtClient> clientEndpoints = new ConcurrentDictionary<string, IAdtClient>();

        public AdtHandler(
          DefaultAzureCredential credential,
          ILoggerFactory loggerFactory,
          IReadOnlyPolicyRegistry<string> policyRegistry,
          IAdtClientUtil adtClientUtil)
        {
            this.credential = credential;
            this.loggerFactory = loggerFactory;
            this.policyRegistry = policyRegistry;
            this.adtClientUtil = adtClientUtil;
        }

        public IAdtClient GetAdtClient(string instanceUrl, bool continueWithErrors)
        {
            return this.clientEndpoints.GetOrAdd(instanceUrl, (instanceUrl) => this.CreateClient(instanceUrl, continueWithErrors));
        }

        private IAdtClient CreateClient(string instanceUrl, bool continueWithErrors)
        {
            DigitalTwinsClient client = new DigitalTwinsClient(new Uri(instanceUrl), this.credential);
            return new AdtClient(client, this.loggerFactory, this.policyRegistry, this.adtClientUtil, continueWithErrors);
        }
    }
}