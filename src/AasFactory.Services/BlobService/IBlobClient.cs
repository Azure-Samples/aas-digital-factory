using System;
using System.Threading.Tasks;

namespace AasFactory.Services
{
    /// <summary>
    /// This interface is meant to provide access to blob content.
    /// </summary>
    public interface IBlobClient
    {
        /// <summary>
        ///  Initializes a new instance of a blob content that holds content returned from a blob storage.
        /// </summary>
        /// <returns>A blob content class that holds blob content.</returns>
        public BlobContent Download();

        /// <summary>
        ///  Initializes a new instance of a blob content that holds content returned from a blob storage.
        /// </summary>
        /// <param name="content">The content to be uploaded to the blob storage.</param>
        /// <param name="overwrite">Whether or not to overwrite an existing blob.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public void Upload(string content, bool overwrite);

        /// <summary>
        /// Deletes the blob.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public void Delete();
    }
}
