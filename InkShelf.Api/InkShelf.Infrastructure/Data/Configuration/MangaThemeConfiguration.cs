using InkShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InkShelf.Infrastructure.Data.Configuration
{
    public class MangaThemeConfiguration : IEntityTypeConfiguration<MangaTheme>
    {
        public void Configure(EntityTypeBuilder<MangaTheme> builder)
        {
            builder.HasKey(mt => mt.Id);

            builder
                .HasIndex(mt => mt.Code)
                .IsUnique();

            builder
                .Property(mt => mt.Code)
                .IsRequired();
            builder
                .Property(mt => mt.Name)
                .IsRequired();
        }
    }
}
