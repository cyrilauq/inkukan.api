using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories;

public interface IUserRepository
{
    Task<User> CreateAsync(User user, string password, CancellationToken cancellationToken);
    Task<User?> FindByCredentials(string login, string password, CancellationToken token);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string pseudo, CancellationToken cancellationToken);
}
