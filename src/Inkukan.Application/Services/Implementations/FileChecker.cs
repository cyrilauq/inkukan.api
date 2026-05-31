using Inkukan.Application.Services;

namespace Inkukan.Application.Services.Implementations
{
    public class FileChecker : IFileChecker
    {
        static readonly List<byte> Jpg = [0xFF, 0xD8];
        static readonly List<byte> Png = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];

        static readonly List<(List<byte> magic, string extension, bool isImage)> ImageFormats =
        [
            (Jpg, "jpg", true),
            (Png, "png", true),
        ];

        public Task<bool> FileByteHasSupportedType(byte[] fileContent, params SupportedFileType[] supportedFileTypes)
        {
            var formatsToCheck = ImageFormats
                .Where(i => Enum.TryParse(typeof(SupportedFileType), i.extension, true, out object? foundType))
                .ToList();
            foreach ((List<byte> magic, string extension, bool isImage) format in formatsToCheck)
            {
                if (IsImage(fileContent, format.magic))
                {
                    if (Enum.Parse(typeof(SupportedFileType), format.extension, true) is SupportedFileType)
                        return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public async Task<bool> FileIsSupportedType(string fileName, byte[] fileContent, params SupportedFileType[] supportedFileTypes)
        {
            return await FileNameHasSupportedType(fileName, supportedFileTypes) && await FileByteHasSupportedType(fileContent, supportedFileTypes);
        }

        public Task<bool> FileNameHasSupportedType(string fileName, params SupportedFileType[] supportedFileTypes)
        {
            string fileExtension = Path.GetExtension(fileName).Replace(".", "");
            if (Enum.TryParse(typeof(SupportedFileType), fileExtension, true, out object? foundType) 
                && foundType is SupportedFileType fileType
                && supportedFileTypes.Contains(fileType))
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        private static bool IsImage(byte[] array, List<byte> comparer, int offset = 0)
        {
            int arrayIndex = offset;
            foreach (byte c in comparer)
            {
                if (arrayIndex > array.Length - 1 || array[arrayIndex] != c)
                    return false;
                ++arrayIndex;
            }
            return true;
        }
    }
}
