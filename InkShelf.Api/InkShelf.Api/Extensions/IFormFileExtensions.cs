using InkShelf.Application.Features.SerieVolume.Create;

namespace InkShelf.Api.Extensions
{
    public static class IFormFileExtensions
    {
        public static async Task<byte[]> GetByteArrayAsync(this IFormFile formFile)
        {
            using MemoryStream stream = new();
            await formFile.CopyToAsync(stream);
            return stream.ToArray();
        }
        public static async Task<FileDto?> ToFileDto(this IFormFile? formFile)
            => formFile == null ? null : new(formFile.FileName, await formFile.GetByteArrayAsync());
    }
}
