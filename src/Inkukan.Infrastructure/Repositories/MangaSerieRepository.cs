using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories
{
    public class MangaSerieRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        :  BaseRepository<MangaSerie>(contextFactory), IMangaSerieRepository
    {

        public new async Task<MangaSerie?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            ApplicationDbContext dbContext = DbContextFactory.CreateDbContext();
            return await dbContext.MangaSeries
                .Include(s => s.Author)
                .Include(s => s.Translator)
                .Include(s => s.Drawer)
                .Include(s => s.EditorVF)
                .Include(s => s.EditorVO)
                .Include(s => s.Collection)
                .Include(s => s.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
