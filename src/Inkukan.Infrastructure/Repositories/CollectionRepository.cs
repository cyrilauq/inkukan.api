using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories;

public class CollectionRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : BaseRepository<MangaCollection>(dbContextFactory), ICollectionRepository
{
}
