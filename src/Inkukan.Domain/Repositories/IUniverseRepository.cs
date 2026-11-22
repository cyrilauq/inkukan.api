using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories;

public interface IUniverseRepository : IBaseRepository<Universe>
{
    Task DeleteWithSeriesAsync(Universe universe, CancellationToken cancellationToken);
}
