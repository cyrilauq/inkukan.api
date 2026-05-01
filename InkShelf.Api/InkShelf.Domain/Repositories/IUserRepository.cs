using InkShelf.Domain.Entities;

namespace InkShelf.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user, string password, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByUsernameAsync(string pseudo, CancellationToken cancellationToken = default);
    }
}
