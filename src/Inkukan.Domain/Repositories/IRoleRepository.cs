using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<string>> GetUserRolesAsync(User user, CancellationToken cancellationToken);
}
