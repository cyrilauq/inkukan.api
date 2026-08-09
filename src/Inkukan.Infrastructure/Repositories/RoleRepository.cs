using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Inkukan.Infrastructure.Repositories;

public class RoleRepository(UserManager<User> userManager) : IRoleRepository
{
    public async Task<IEnumerable<string>> GetUserRolesAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await userManager.GetRolesAsync(user);
    }
}
