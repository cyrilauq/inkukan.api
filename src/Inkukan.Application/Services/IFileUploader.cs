namespace Inkukan.Application.Services
{
    public interface IFileUploader
    {
        Task<Guid?> UploadAsync(string fileName, byte[] content, string outPutFileName, params SupportedFileType[] supportedFileTypes);
    }

    [Flags]
    public enum SupportedFileType
    {
        PNG = 0,
        JPEG = 1,
        JPG = 2
    }
}
