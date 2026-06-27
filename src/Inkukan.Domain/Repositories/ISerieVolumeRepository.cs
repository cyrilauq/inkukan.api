using Inkukan.Domain.Entities;

namespace Inkukan.Domain.Repositories
{
    public interface ISerieVolumeRepository : IBaseRepository<SerieVolume>
    {
        Task<SerieVolume?> GetBySerieIdAndVolumeNumber(Guid serieId, int volumeNumber, CancellationToken cancellationToken = default);
    }
}
