using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration
{
    public class MangaCollectionConfiguration : IEntityTypeConfiguration<MangaCollection>
    {
        public void Configure(EntityTypeBuilder<MangaCollection> builder)
        {
            builder.HasKey(mc => mc.Id);

            builder.HasIndex(mc => mc.Code)
                .IsUnique();

            builder
                .Property(mc => mc.Code)
                .IsRequired();
            builder
                .Property(mc => mc.Name)
                .IsRequired();
        }
    }
}
