namespace Inkukan.Domain.Repositories
{
    public interface IBlobStorage
    {
        /// <summary>
        /// Save a file to a blob storage
        /// </summary>
        /// <param name="content">File content</param>
        /// <returns>The filename of the savedFile</returns>
        Task<string> UploadAsync(byte[] content, string filePath);
    }
}
