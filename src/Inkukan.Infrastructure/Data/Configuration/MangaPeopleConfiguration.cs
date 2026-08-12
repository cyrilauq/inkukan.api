using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration
{
    public class MangaPeopleConfiguration : IEntityTypeConfiguration<MangaPeople>
    {
        public void Configure(EntityTypeBuilder<MangaPeople> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .HasIndex(x => new { x.Firstname, x.Lastname })
                .IsUnique();

            builder
                .Property(mp => mp.Lastname)
                .IsRequired();
            builder
                .Property(mp => mp.Firstname)
                .IsRequired();
        }
    }
}
