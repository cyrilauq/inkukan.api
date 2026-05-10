using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Infrastructure.Repositories
{
    public class MangaSerieRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        :  BaseRepository<MangaSerie>(contextFactory), IMangaSerieRepository
    {
    }
}
