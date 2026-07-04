namespace Inkukan.Application.Services;

public interface IFileChecker
{
    bool FileByteHasSupportedType(byte[] fileContent, params SupportedFileType[] supportedFileTypes);
    bool FileIsSupportedType(string fileName, byte[] fileContent, params SupportedFileType[] supportedFileTypes);
    bool FileNameHasSupportedType(string fileName, params SupportedFileType[] supportedFileTypes);
}
