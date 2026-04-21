using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Infrastructure.Repositories
{
    public class SerieVolumeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : BaseRepository<SerieVolume>(dbContextFactory), ISerieVolumeRepository
    {

        public async Task<SerieVolume?> GetBySerieIdAndVolumeNumber(Guid serieId, int volumeNumber)
        {
            ApplicationDbContext context = await dbContextFactory.CreateDbContextAsync();
            return await context
                .SerieVolumes
                .Where(sv => sv.MangaSerieId == serieId && sv.VolumeNumber == volumeNumber)
                .FirstOrDefaultAsync();
        }
    }
}
