namespace AasFactory.Services
{
    /// <summary>
    /// This provides an abstraction over Blob to allow mocking.
    /// </summary>
    public interface IBlob
    {
        /// <summary>
        /// Gets the filename of the blob.
        /// </summary>
        public string Filename { get; }
    }
}