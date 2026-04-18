using InkShelf.Domain.Entities;
using InkShelf.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Reflection.Emit;

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
        public DbSet<SerieVolume> SerieVolumes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ConfigurateITrackableEntity(builder);
            ConfigurateILogicalDeleteEntity(builder);
        }

        private static void ConfigurateILogicalDeleteEntity(ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ILogicalDelete).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .Property(nameof(ILogicalDelete.IsDeleted))
                        .HasComputedColumnSql($@"""{nameof(ILogicalDelete.DeletedAt)}"" IS NOT NULL", stored: true);
                }
            }
        }

        private static void ConfigurateITrackableEntity(ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ITrackableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .HasKey(nameof(ITrackableEntity.Id));
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry entry in entries)
            {
                if ((entry.State == EntityState.Modified || entry.State == EntityState.Added) && entry.Entity is ITrackableEntity updatedEntity)
                    updatedEntity.UpdatedAt = now;
                if (entry.State == EntityState.Added && entry.Entity is ITrackableEntity createdEntity)
                    createdEntity.CreatedAt = now;
                if (entry.State == EntityState.Deleted && entry.Entity is ILogicalDelete deletedEntity)
                {
                    entry.State = EntityState.Modified;
                    deletedEntity.DeletedAt = now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
