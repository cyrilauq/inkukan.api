using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InkShelf.Infrastructure.Repositories
{
    public class MangaPeopleRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IMangaPeopleRepository
    {
        public async Task<MangaPeople> CreateAsync(MangaPeople entity)
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            EntityEntry<MangaPeople> addResult = dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
            return addResult.Entity;
        }

        public IQueryable<MangaPeople> GetQuery()
        {
            ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();
            return dbContext.MangaPeoples;
        }
    }
}
