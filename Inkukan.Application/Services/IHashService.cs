namespace Inkukan.Application.Services
{
    public interface IHashService
    {
        Task<string> HashBytesAsync(byte[] bytes);
        Task<bool> VerifyHashAsync(string? hashedBytes,  byte[] bytes);
    }
}
