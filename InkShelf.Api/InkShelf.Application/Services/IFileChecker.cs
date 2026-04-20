namespace InkShelf.Application.Services
{
    public interface IFileChecker
    {
        Task<bool> FileByteHasSupportedType(byte[] fileContent, params SupportedFileType[] supportedFileTypes);
        Task<bool> FileIsSupportedType(string fileName, byte[] fileContent, params SupportedFileType[] supportedFileTypes);
        Task<bool> FileNameHasSupportedType(string fileName, params SupportedFileType[] supportedFileTypes);
    }
}
