using InkShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InkShelf.Infrastructure.Data.Configuration
{
    public class MangaSerieConfiguration : IEntityTypeConfiguration<MangaSerie>
    {
        public void Configure(EntityTypeBuilder<MangaSerie> builder)
        {
            builder.HasKey(ms => ms.Id);

            builder
                .HasIndex(ms => ms.TitleVO)
                .IsUnique();
            builder
                .HasIndex(ms => ms.TitleVF)
                .IsUnique();

            builder
                .Property(ms => ms.TitleVO)
                .IsRequired();
            builder
                .Property(ms => ms.TitleVF)
                .IsRequired();
            builder
                .Property(ms => ms.TotalVolumes)
                .IsRequired();
            builder
                .Property(ms => ms.Synopsis)
                .IsRequired();

            #region Constraints
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("TotalVolumesShouldNotBeNegative", "TotalVolumes > -1");
            });
            #endregion

            #region Relationships
            builder
                .HasOne(ms => ms.Author)
                .WithMany()
                .HasForeignKey(ms => ms.AuthorId);
            builder
                .HasOne(ms => ms.Author)
                .WithMany()
                .HasForeignKey(ms => ms.AuthorId);
            builder
                .HasOne(ms => ms.Translator)
                .WithMany()
                .HasForeignKey(ms => ms.TranslatorId);
            builder
                .HasOne(ms => ms.EditorVF)
                .WithMany()
                .HasForeignKey(ms => ms.EditorVFId);
            builder
                .HasOne(ms => ms.EditorVO)
                .WithMany()
                .HasForeignKey(ms => ms.EditorVOId);
            builder
                .HasOne(ms => ms.Collection)
                .WithMany(a => a.Mangas)
                .HasForeignKey(ms => ms.CollectionId);
            builder
                .HasOne(ms => ms.Type)
                .WithMany(a => a.Mangas)
                .HasForeignKey(ms => ms.TypeId);

            builder
                .HasMany(ms => ms.Themes)
                .WithMany(t => t.Mangas);
            #endregion
        }
    }
}
