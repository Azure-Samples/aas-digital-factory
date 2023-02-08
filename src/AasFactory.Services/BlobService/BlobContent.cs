using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AasFactory.Services
{
    /// <summary>
    /// This object meant to hold blob content returned from a blob storage.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BlobContent
    {
        /// <summary>
        /// Gets the string content of the blob.
        /// </summary>
        public string Content
        {
            get => Encoding.UTF8.GetString(this.RawContent);
        }

        /// <summary>
        /// Gets or sets the byte array. raw content of the blob.
        /// </summary>
        public byte[] RawContent { get; set; } = new byte[0];
    }
}