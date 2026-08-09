using Microsoft.AspNetCore.Identity;

namespace Inkukan.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];

    public ICollection<UserListItem> List { get; set; } = [];
}
