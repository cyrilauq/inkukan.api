using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories;

public class SerieVolumeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : BaseRepository<SerieVolume>(dbContextFactory), ISerieVolumeRepository
{

    public async Task<SerieVolume?> GetBySerieIdAndVolumeNumberAsync(Guid serieId, int volumeNumber, CancellationToken cancellationToken)
    {
        using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext
            .SerieVolumes
            .Where(sv => sv.MangaSerieId == serieId && sv.VolumeNumber == volumeNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IQueryable<SerieVolume>> GetByUserAndListAsync(Guid userId, UserListType userListType, CancellationToken cancellationToken)
    {
        ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        List<Guid> listItemIds = await dbContext
            .UserListItems
            .Where(uli => uli.UserId == userId && uli.Type == userListType)
            .Select(uli => uli.VolumeId)
            .ToListAsync(cancellationToken);
        return dbContext
            .SerieVolumes
            .Where(sv => listItemIds.Contains(sv.Id));
    }
}
