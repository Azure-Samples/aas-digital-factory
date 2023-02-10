namespace AasFactory.Services
{
    /// <summary>
    /// This interface is meant to provide access to a storage client.
    /// </summary>
    public interface IBlobStorageHandler
    {
        /// <summary>
        ///  Initializes a new instance of a blob service client.
        /// </summary>
        /// <param name="connection">The uri or connection string that represents the blob storage endpoint.</param>
        /// <returns>An interface for a blob service client.</returns>
        public IBlobServiceClient GetBlobServiceClient(string connection);
    }
}