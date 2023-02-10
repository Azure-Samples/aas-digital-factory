using System.Diagnostics.CodeAnalysis;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Polly.Registry;

namespace AasFactory.Services
{
    /// <summary>
    /// This class connects to an Azure.Storage.Blobs.BlobServiceClient.
    /// Note: When including this class in the service collection, do so as a Singleton.
    /// The credentials field may be reused for multiple calls to GetBlobServiceClient.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BlobStorageHandler : IBlobStorageHandler
    {
        private readonly DefaultAzureCredential credential;
        private readonly ILoggerFactory loggerFactory;
        private readonly IReadOnlyPolicyRegistry<string> policyRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobStorageHandler"/> class.
        /// </summary>
        /// <param name="credential">An azure credential.</param>
        /// <param name="loggerFactory">A logger factory.</param>
        /// <param name="policyRegistry">The Polly policy DI container.</param>
        public BlobStorageHandler(DefaultAzureCredential credential, ILoggerFactory loggerFactory, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            this.credential = credential;
            this.loggerFactory = loggerFactory;
            this.policyRegistry = policyRegistry;
        }

        /// <inheritdoc />
        public IBlobServiceClient GetBlobServiceClient(string connection)
        {
            return new BlobServiceClient(connection, this.credential, this.loggerFactory, this.policyRegistry);
        }
    }
}