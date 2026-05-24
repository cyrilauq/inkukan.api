using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace Inkukan.Application.Extensions
{
    public static class ImageExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this Image image, IImageEncoder encoder)
        {
            using MemoryStream outStream = new();

            await image.SaveAsync(outStream, encoder);
            outStream.Seek(0, SeekOrigin.Begin);

            return outStream.ToArray();
        }

        public static Image ResizeImage(this Image image, int width, int? height = null)
            => image.Clone(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height ?? 0),
                Mode = ResizeMode.Max
            }));
    }
}
