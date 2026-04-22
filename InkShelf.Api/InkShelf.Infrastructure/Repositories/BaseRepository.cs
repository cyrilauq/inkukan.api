using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class BaseRepository<T>(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IBaseRepository<T> where T : class
    {
        protected IDbContextFactory<ApplicationDbContext> DbContextFactory = dbContextFactory;
        protected ApplicationDbContext DbContext { get => DbContextFactory.CreateDbContext(); }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<T> result = DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>()
                .FindAsync(id, cancellationToken);
        }

        public IQueryable<T> GetQuery()
        {
            return DbContext.Set<T>();
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<T> result = DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }
    }
}
