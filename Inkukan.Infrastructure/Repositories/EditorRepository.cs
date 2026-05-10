using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Infrastructure.Repositories
{
    public class EditorRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : BaseRepository<Editor>(dbContextFactory), IEditorRepository
    {
        public async Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            ApplicationDbContext dbContext = DbContextFactory.CreateDbContext();
            return await dbContext.Editors
                .Where(e => e.Name == name)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
