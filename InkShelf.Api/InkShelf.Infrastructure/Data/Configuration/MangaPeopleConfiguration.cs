using InkShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InkShelf.Infrastructure.Data.Configuration
{
    public class MangaPeopleConfiguration : IEntityTypeConfiguration<MangaPeople>
    {
        public void Configure(EntityTypeBuilder<MangaPeople> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(mp => mp.Lastname)
                .IsRequired();
            builder
                .Property(mp => mp.Firstname)
                .IsRequired();
        }
    }
}
