using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class MangaSerieRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IMangaSerieRepository
    {
        public async Task<MangaSerie> CreateAsync(MangaSerie entity)
        {
            ApplicationDbContext context = contextFactory.CreateDbContext();
            EntityEntry<MangaSerie> result = await context.AddAsync(entity);

            await context.SaveChangesAsync();

            return result.Entity;
        }

        public IQueryable<MangaSerie> GetQuery()
        {
            ApplicationDbContext context = contextFactory.CreateDbContext();
            return context.MangaSeries;
        }
    }
}
