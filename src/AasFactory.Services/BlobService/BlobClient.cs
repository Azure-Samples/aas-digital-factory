using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AasFactory.Services.Logging;
using AasFactory.Services.Utils;
using Azure;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;
using AzureStorageBlob = Azure.Storage.Blobs;

namespace AasFactory.Services
{
    /// <summary>
    /// This class connects to an Azure.Storage.BlobClient.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BlobClient : IBlobClient
    {
        private readonly AzureStorageBlob.BlobClient blobClient;
        private readonly ISyncPolicy policy;
        private readonly ILogger<BlobClient> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobClient"/> class.
        /// </summary>
        /// <param name="containerName">The container that represents the beginning of the blob storage file path.</param>
        /// <param name="fileName">The path of the file within the container.</param>
        /// <param name="blobServiceClient">The Azure.Storage.Blobs.BlobServiceClient instance.</param>
        /// <param name="logger">A logger.</param>
        /// <param name="policyRegistry">The Polly policy DI container.</param>
        public BlobClient(
            string containerName,
            string fileName,
            AzureStorageBlob.BlobServiceClient blobServiceClient,
            ILogger<BlobClient> logger,
            IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            this.blobClient = blobContainerClient.GetBlobClient(fileName);
            this.logger = logger;
            this.policy = policyRegistry.Get<ISyncPolicy>(PolicyExtensions.StorageAccountPolicyName);
        }

        /// <inheritdoc />
        public BlobContent Download()
        {
            var watch = Stopwatch.StartNew();
            this.logger.DownloadingFromBlob(this.blobClient.BlobContainerName, this.blobClient.Name);

            var response = this.policy!.Execute(() =>
                this.blobClient.DownloadContent());

            var blobContent = new BlobContent
            {
                RawContent = response.Value.Content.ToArray(),
            };

            this.logger.DownloadedFromBlob(blobContent.RawContent.Length, this.blobClient.BlobContainerName, this.blobClient.Name, watch.ElapsedMilliseconds);
            return blobContent;
        }

        /// <inheritdoc />
        public void Upload(string content, bool overwrite)
        {
            var watch = Stopwatch.StartNew();
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            this.logger.UploadingToBlob(content?.Length ?? 0, this.blobClient.BlobContainerName, this.blobClient.Name);
            this.policy!.Execute(() =>
                this.blobClient.Upload(stream, overwrite));

            watch.Stop();
            this.logger.UploadedToBlob(this.blobClient.BlobContainerName, this.blobClient.Name, watch.ElapsedMilliseconds);
        }

        /// <inheritdoc/>
        public void Delete()
        {
            var watch = Stopwatch.StartNew();
            this.logger.DeletingBlob(this.blobClient.BlobContainerName, this.blobClient.Name);
            this.blobClient.Delete(AzureStorageBlob.Models.DeleteSnapshotsOption.IncludeSnapshots);
            watch.Stop();
            this.logger.DeletedBlob(this.blobClient.BlobContainerName, this.blobClient.Name, watch.ElapsedMilliseconds);
        }
    }
}