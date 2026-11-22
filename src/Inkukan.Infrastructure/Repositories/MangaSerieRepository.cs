using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inkukan.Infrastructure.Repositories;

public class MangaSerieRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    :  BaseRepository<MangaSerie>(contextFactory), IMangaSerieRepository
{

    public new async Task<MangaSerie?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.MangaSeries
            .Include(s => s.Author)
            .Include(s => s.Translator)
            .Include(s => s.Drawer)
            .Include(s => s.EditorVF)
            .Include(s => s.EditorVO)
            .Include(s => s.Collection)
            .Include(s => s.Type)
            .Include(s => s.Universe)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<MangaSerie?> GetByIdWithoutDependenciesAsync(Guid id, CancellationToken cancellationToken)
    {
        using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.MangaSeries
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public override async Task<MangaSerie> CreateAsync(MangaSerie entity, CancellationToken cancellationToken)
    {
        await SaveWithLockedUniverseAsync(entity, (dbContext, serie) => dbContext.MangaSeries.Add(serie), cancellationToken);
        return await GetByIdAsync(entity.Id, cancellationToken) ?? entity;
    }

    public override async Task<MangaSerie> UpdateAsync(MangaSerie entity, CancellationToken cancellationToken)
    {
        await SaveWithLockedUniverseAsync(entity, (dbContext, serie) =>
            dbContext.MangaSeries.Update(serie).Property(s => s.DeletedAt).IsModified = false, cancellationToken);
        return await GetByIdAsync(entity.Id, cancellationToken) ?? entity;
    }

    /// <summary>
    /// Saves the serie inside a transaction that locks its universe row with <c>FOR SHARE</c>.
    /// A concurrent universe deletion has to wait for this save to commit, so it also soft-deletes this serie.
    /// If the deletion commits first, the universe is no longer found and an <see cref="EntityNotFoundException"/> is thrown.
    /// This prevents an active serie from being attached to a deleted universe.
    /// </summary>
    private async Task SaveWithLockedUniverseAsync(MangaSerie serie, Action<ApplicationDbContext, MangaSerie> attach, CancellationToken cancellationToken)
    {
        await using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        // FOR SHARE locks the universe row until the transaction ends: other serie saves can still take the same lock,
        // but a universe deletion (an UPDATE on this row) waits. If the deletion committed first, Postgres re-checks
        // NOT "IsDeleted" on the latest row version, so no row is returned.
        List<Universe> universes = await dbContext.Universes
            .FromSql($"""SELECT * FROM "Universes" WHERE "Id" = {serie.UniverseId} AND NOT "IsDeleted" FOR SHARE""")
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        if (universes.Count == 0)
            throw new EntityNotFoundException($"No Universe with the id [{serie.UniverseId}] found");

        attach(dbContext, serie);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
