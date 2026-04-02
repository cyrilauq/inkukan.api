using InkShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InkShelf.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext(options)
    {
        public DbSet<Editor> Editors { get; set; }
        public DbSet<MangaSerie> MangaSeries { get; set; }
        public DbSet<MangaCollection> MangaCollections { get; set; }
        public DbSet<MangaTheme> MangaThemes { get; set; }
        public DbSet<MangaPeople> MangaPeoples { get; set; }
        public DbSet<MangaType> MangaTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
