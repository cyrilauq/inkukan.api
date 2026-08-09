using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories;

public interface ISerieVolumeRepository : IBaseRepository<SerieVolume>
{
    Task<SerieVolume?> GetBySerieIdAndVolumeNumberAsync(Guid serieId, int volumeNumber, CancellationToken cancellationToken);
}
