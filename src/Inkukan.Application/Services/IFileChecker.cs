namespace Inkukan.Application.Services
{
    public interface IFileChecker
    {
        Task<bool> FileByteHasSupportedType(byte[] fileContent, CancellationToken cancellationToken = default, params SupportedFileType[] supportedFileTypes);
        Task<bool> FileIsSupportedType(string fileName, byte[] fileContent, CancellationToken cancellationToken = default, params SupportedFileType[] supportedFileTypes);
        Task<bool> FileNameHasSupportedType(string fileName, CancellationToken cancellationToken = default, params SupportedFileType[] supportedFileTypes);
    }
}
