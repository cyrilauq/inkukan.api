using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class EditorRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IEditorRepository
    {
        public async Task<Editor> CreateAsync(Editor entity)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            EntityEntry<Editor> addResult = dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
            return addResult.Entity;
        }

        public async Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
            return await dbContext.Editors
                .Where(e => e.Name == name)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<Editor> GetQuery()
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            return dbContext.Editors;
        }
    }
}
