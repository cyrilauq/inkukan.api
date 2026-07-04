using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories
{
    public class SerieVolumeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : BaseRepository<SerieVolume>(dbContextFactory), ISerieVolumeRepository
    {

        public async Task<SerieVolume?> GetBySerieIdAndVolumeNumberAsync(Guid serieId, int volumeNumber, CancellationToken cancellationToken)
        {
            ApplicationDbContext dbContext = DbContextFactory.CreateDbContext();
            return await dbContext
                .SerieVolumes
                .Where(sv => sv.MangaSerieId == serieId && sv.VolumeNumber == volumeNumber)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
