using InkShelf.Domain.Entities;

namespace InkShelf.Domain.Repositories
{
    public interface ISerieVolumeRepository : IBaseRepository<SerieVolume>
    {
        Task<SerieVolume?> GetBySerieIdAndVolumeNumber(Guid serieId, int volumeNumber);
    }
}
