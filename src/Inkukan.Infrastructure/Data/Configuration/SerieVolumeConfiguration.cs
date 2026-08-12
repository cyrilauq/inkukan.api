using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration
{
    public class SerieVolumeConfiguration : IEntityTypeConfiguration<SerieVolume>
    {
        public void Configure(EntityTypeBuilder<SerieVolume> builder)
        {
            builder
                .Property(b => b.VolumeNumber)
                .IsRequired();
            builder
                .Property(b => b.Synopsis)
                .IsRequired()
                .HasMaxLength(255);
            builder
                .Property(b => b.VFCoverPath)
                .HasMaxLength(255);
            builder
                .Property(b => b.VOCoverPath)
                .HasMaxLength(255);
            builder
                .Property(b => b.VOParutionDate)
                .IsRequired();
            builder
                .Property(b => b.VFParutionDate);
            //builder
            //    .Property(b => b.VFParutionCountry)
            //    .HasMaxLength(100);
            //builder
            //    .Property(b => b.VOParutionCountry)
            //    .IsRequired()
            //    .HasMaxLength(100);
            builder
                .Property(b => b.RecommendedAge)
                .IsRequired();
            builder
                .Property(b => b.EANCode)
                .HasMaxLength(25);
            builder
                .Property(b => b.PriceCode)
                .HasMaxLength(25);
            builder
                .Property(b => b.MangaSerieId)
                .IsRequired();

            builder
                .HasOne(b => b.MangaSerie)
                .WithMany(ms => ms.Volumes)
                .HasForeignKey(sv => sv.MangaSerieId);
        }
    }
}
