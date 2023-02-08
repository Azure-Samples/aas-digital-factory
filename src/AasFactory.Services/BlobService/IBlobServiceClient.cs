using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AasFactory.Services
{
    /// <summary>
    /// This interface is meant to provide access to a blob client.
    /// </summary>
    public interface IBlobServiceClient
    {
        /// <summary>
        /// Gets a new instance of a blob client.
        /// </summary>
        /// <param name="containerName">The container that represents the beginning of the blob storage file path.</param>
        /// <param name="fileName">The path of the file within the container.</param>
        /// <returns>An interface for a blob client.</returns>
        public IBlobClient GetBlobClient(string containerName, string fileName);
    }
}