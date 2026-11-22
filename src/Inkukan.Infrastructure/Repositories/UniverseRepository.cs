using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inkukan.Infrastructure.Repositories;

public class UniverseRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : BaseRepository<Universe>(dbContextFactory), IUniverseRepository
{
    public async Task DeleteWithSeriesAsync(Universe universe, CancellationToken cancellationToken)
    {
        await using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Universes.Remove(universe);
        await dbContext.SaveChangesAsync(cancellationToken);

        DateTimeOffset now = DateTimeOffset.UtcNow;
        await dbContext.MangaSeries
            .Where(serie => serie.UniverseId == universe.Id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(serie => serie.DeletedAt, now), cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}
