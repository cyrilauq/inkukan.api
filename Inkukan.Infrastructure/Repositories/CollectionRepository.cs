using InkShelf.Domain.Entities;
using InkShelf.Infrastructure.Data;
using InkShelf.Infrastructure.Repositories;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories
{
    public class CollectionRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        : BaseRepository<MangaCollection>(dbContextFactory), ICollectionRepository
    {
    }
}
