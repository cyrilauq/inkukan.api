using Microsoft.AspNetCore.Identity;

namespace Inkukan.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public DateTime CreatedAt { get;  set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
}
