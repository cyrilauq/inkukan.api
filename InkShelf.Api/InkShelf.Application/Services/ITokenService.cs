namespace InkShelf.Application.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenForUserAsync(Domain.Entities.User user);
    }
}
