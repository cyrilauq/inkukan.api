using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class EditorRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IEditorRepository
    {
        public async Task<Editor> CreateAsync(Editor entity, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            EntityEntry<Editor> addResult = dbContext.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return addResult.Entity;
        }

        public Task<Editor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await dbContext.Editors
                .Where(e => e.Name == name)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<Editor> GetQuery()
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            return dbContext.Editors;
        }

        public Task<Editor> UpdateAsync(Editor entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
