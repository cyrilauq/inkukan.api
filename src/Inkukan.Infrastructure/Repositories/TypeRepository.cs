using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Infrastructure.Repositories;

public class TypeRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) 
    : BaseRepository<MangaType>(dbContextFactory), ITypeRepository
{
}
