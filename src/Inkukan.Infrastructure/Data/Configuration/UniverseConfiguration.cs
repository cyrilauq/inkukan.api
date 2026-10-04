using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration;

public class UniverseConfiguration : IEntityTypeConfiguration<Universe>
{
    public void Configure(EntityTypeBuilder<Universe> builder)
    {
        builder.HasKey(universe => universe.Code);
        builder.HasIndex(universe => universe.Code).IsUnique();
        builder.Property(universe => universe.Name).IsRequired();
        builder.Property(universe => universe.Code).IsRequired();
        builder.HasMany(universe => universe.Mangas)
            .WithOne(manga => manga.Universe)
            .HasForeignKey(manga => manga.UniverseId)
            .IsRequired();
    }
}
