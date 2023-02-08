using System;
using System.Diagnostics.CodeAnalysis;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using AzureStorageBlobs = Azure.Storage.Blobs;

namespace AasFactory.Services
{
    /// <summary>
    /// This class is meant to provide access to a blob client.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BlobServiceClient : IBlobServiceClient
    {
        private readonly AzureStorageBlobs.BlobServiceClient blobServiceClient;
        private readonly ILoggerFactory loggerFactory;
        private readonly IReadOnlyPolicyRegistry<string> policyRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobServiceClient"/> class.
        /// The constructor for the BlobServiceClient that connects to an Azure.Storage.Blobs.BlobServiceClient.
        /// </summary>
        /// <param name="connection">The connection string or uri to use for connecting to the Azure Blob Service.</param>
        /// <param name="credential">A credential.</param>
        /// <param name="loggerFactory">A logger factory.</param>
        /// <param name="policyRegistry">The Polly policy DI container.</param>
        public BlobServiceClient(string connection, DefaultAzureCredential credential, ILoggerFactory loggerFactory, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            if (connection.Split(";").Length > 1)
            {
                this.blobServiceClient = new AzureStorageBlobs.BlobServiceClient(connection);
            }
            else
            {
                this.blobServiceClient = new AzureStorageBlobs.BlobServiceClient(new Uri(connection), credential);
            }

            this.loggerFactory = loggerFactory;
            this.policyRegistry = policyRegistry;
        }

        /// <inheritdoc />
        public IBlobClient GetBlobClient(string containerName, string fileName)
        {
            return new BlobClient(containerName, fileName, this.blobServiceClient, this.loggerFactory.CreateLogger<BlobClient>(), this.policyRegistry);
        }
    }
}