using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class SerieVolumeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : ISerieVolumeRepository
    {
        public async Task<SerieVolume> CreateAsync(SerieVolume entity)
        {
            ApplicationDbContext context = dbContextFactory.CreateDbContext();
            EntityEntry<SerieVolume> addResult = context.Add(entity);
            await context.SaveChangesAsync();

            return addResult.Entity;
        }

        public async Task<SerieVolume?> GetBySerieIdAndVolumeNumber(Guid serieId, int volumeNumber)
        {
            ApplicationDbContext context = await dbContextFactory.CreateDbContextAsync();
            return await context
                .SerieVolumes
                .Where(sv => sv.MangaSerieId == serieId && sv.VolumeNumber == volumeNumber)
                .FirstOrDefaultAsync();
        }

        public IQueryable<SerieVolume> GetQuery()
        {
            ApplicationDbContext context = dbContextFactory.CreateDbContext();
            return context.SerieVolumes;
        }
    }
}
