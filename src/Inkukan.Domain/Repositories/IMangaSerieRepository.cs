using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories;

public interface IMangaSerieRepository : IBaseRepository<MangaSerie>
{
    Task<MangaSerie?> GetByIdWithoutDependenciesAsync(Guid id, CancellationToken cancellationToken);
}
