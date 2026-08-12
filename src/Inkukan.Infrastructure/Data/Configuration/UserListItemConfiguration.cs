using Inkukan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inkukan.Infrastructure.Data.Configuration
{
    public class UserListItemConfiguration : IEntityTypeConfiguration<UserListItem>
    {
        public void Configure(EntityTypeBuilder<UserListItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Volume)
                .WithMany(v => v.UserLists)
                .HasForeignKey(e => e.VolumeId);
            builder.HasOne(e => e.User)
                .WithMany(u => u.List)
                .HasForeignKey(e => e.UserId);

            builder.Property(e => e.Type)
                .HasConversion<string>();
        }
    }
}
