using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories;

public class EditorRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : BaseRepository<Editor>(dbContextFactory), IEditorRepository
{
    public async Task<Editor?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        using ApplicationDbContext dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Editors
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
