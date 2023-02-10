using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AasFactory.Services
{
    /// <summary>
    /// An implementation of IBlob that abstracts a BlobItem.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Blob : IBlob
    {
        private readonly BlobItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Blob"/> class.
        /// </summary>
        /// <param name="item">The blobitem.</param>
        public Blob(BlobItem item)
        {
            this.item = item;
        }

        /// <inheritdoc/>
        public string Filename => this.item.Name;
    }
}