using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class BaseRepository<T>(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IBaseRepository<T> where T : class
    {
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            EntityEntry<T> result = dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Set<T>()
                .FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            return dbContext.Set<T>();
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            EntityEntry<T> result = dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }
    }
}
