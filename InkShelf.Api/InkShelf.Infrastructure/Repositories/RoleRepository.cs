using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace InkShelf.Infrastructure.Repositories
{
    public class RoleRepository(UserManager<User> userManager) : IRoleRepository
    {
        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
            => await userManager.GetRolesAsync(user);
    }
}
