using InkShelf.Domain.Entities;

namespace InkShelf.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetUserRolesAsync(User user);
    }
}
