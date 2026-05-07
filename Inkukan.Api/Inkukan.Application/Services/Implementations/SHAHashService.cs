
using System.Security.Cryptography;

namespace InkShelf.Application.Services.Implementations
{
    public class SHAHashService : IHashService
    {
        public Task<string> HashBytesAsync(byte[] bytes)
        {
            string fileHash;
            byte[] hashBytes = SHA256.HashData(bytes);
            fileHash = Convert.ToHexString(hashBytes);
            return Task.FromResult(fileHash);
        }

        public Task<bool> VerifyHashAsync(string? hashedBytes, byte[] bytes)
        {
            if(string.IsNullOrEmpty(hashedBytes) || bytes.Length == 0) return Task.FromResult(false);
            string fileHash;
            byte[] hashBytes = SHA256.HashData(bytes);
            fileHash = Convert.ToHexString(hashBytes);
            return Task.FromResult(fileHash.Equals(hashedBytes));
        }
    }
}
