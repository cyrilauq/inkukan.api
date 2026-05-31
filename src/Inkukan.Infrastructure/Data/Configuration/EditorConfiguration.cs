using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration
{
    public class EditorConfiguration : IEntityTypeConfiguration<Editor>
    {
        public void Configure(EntityTypeBuilder<Editor> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasIndex(e => e.Name)
                .IsUnique();

            builder
                .Property(e => e.Name)
                .IsRequired();
        }
    }
}
