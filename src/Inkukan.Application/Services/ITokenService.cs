namespace Inkukan.Application.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenForUserAsync(Domain.Entities.User user, CancellationToken cancellationToken = default);
    }
}
