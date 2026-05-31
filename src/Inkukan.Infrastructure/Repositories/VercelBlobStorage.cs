using Inkukan.Domain.Repositories;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Inkukan.Infrastructure.Repositories
{
    public class VercelBlobStorage(IHttpClientFactory httpClientFactory) : IBlobStorage
    {
        private HttpClient _vercelBlobClient => httpClientFactory.CreateClient("VercelBlocClient");
        public async Task<string> UploadAsync(byte[] content, string filePath)
        {
            using HttpRequestMessage request = new(HttpMethod.Put, filePath);

            using MemoryStream outStream = new();
            outStream.Write(content, 0, content.Length);
            outStream.Seek(0, SeekOrigin.Begin);

            StreamContent streamContent = new(outStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/webp");
            request.Content = streamContent;

            HttpResponseMessage response = await _vercelBlobClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return Path.GetFileName(filePath);
        }
    }

    public class VercelBlobOptions
    {
        public string Token { get; set; } = string.Empty;
        public string BlobUrl { get; set; } = string.Empty;
    }
}
