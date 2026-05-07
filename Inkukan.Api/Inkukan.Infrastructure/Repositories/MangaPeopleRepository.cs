using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Infrastructure.Repositories
{
    public class MangaPeopleRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : BaseRepository<MangaPeople>(dbContextFactory), IMangaPeopleRepository
    {
    }
}
