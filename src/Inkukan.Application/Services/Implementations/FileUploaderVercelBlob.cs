using Inkukan.Application.Extensions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace Inkukan.Application.Services.Implementations
{
    public class FileUploaderVercelBlob(IFileChecker fileChecker, IBlobStorage blobStorage) : IFileUploader
    {
        private readonly Dictionary<string, int> _fileSizes = new()
        {
            { "small", 200 },
            { "medium", 400 },
            { "large", 600 }
        };
        private readonly WebpEncoder _webpEncoder = new() { Quality = 80 };


        public async Task<Guid?> UploadAsync(string fileName, byte[] content, string outPutFileName, params SupportedFileType[] supportedFileTypes)
        {
            await EnsureFileIsSupportedType(fileName, content, supportedFileTypes);

            Guid fileId = Guid.NewGuid();

            using MemoryStream contentStream = new(content);
            using Image originalImage = await Image.LoadAsync(contentStream);

            foreach (var size in _fileSizes)
            {
                using Image resizedImage = originalImage.ResizeImage(size.Value);

                await blobStorage.UploadAsync(await resizedImage.ToByteArrayAsync(_webpEncoder), $"{size.Key}/{fileId}.webp");
            }

            string requestOriginalUri = $"original/{fileId}.{Path.GetExtension(fileName)}";

            using MemoryStream requestOriginalStream = new(content);
            requestOriginalStream.Seek(0, SeekOrigin.Begin);

            await blobStorage.UploadAsync(requestOriginalStream.ToArray(), requestOriginalUri);

            return fileId;
        }

        private async Task EnsureFileIsSupportedType(string fileName, byte[] content, params SupportedFileType[] supportedFileTypes)
        {
            if (await fileChecker.FileIsSupportedType(fileName, content, supportedFileTypes)) return;
            throw new EntityValidationException(
                $"An error occured while validating the file",
                [$"The given file isn't of type [{string.Join(",", supportedFileTypes.Select(s => s.ToString()).ToList())}]"]);
        }
    }

    public class VercelBlobOptions
    {
        public string Token { get; set; } = string.Empty;
        public string BlobUrl { get; set; } = string.Empty;
    }
}
