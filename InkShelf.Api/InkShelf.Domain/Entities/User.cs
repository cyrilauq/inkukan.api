using InkShelf.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InkShelf.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
