using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Inkukan.Infrastructure.Repositories;

public class BaseRepository<T>(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IBaseRepository<T> where T : class
{
    protected IDbContextFactory<ApplicationDbContext> DbContextFactory = dbContextFactory;
    protected ApplicationDbContext DbContext { get => DbContextFactory.CreateDbContext(); }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        EntityEntry<T> result = dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        dbContext.Set<T>().Remove(entity);
        return await dbContext.SaveChangesAsync(cancellationToken) == 1;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Set<T>()
            .FindAsync(id, cancellationToken);
    }

    public IQueryable<T> GetQuery()
    {
        ApplicationDbContext dbContext = DbContextFactory.CreateDbContext();
        return dbContext.Set<T>();
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        EntityEntry<T> result = dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
}
