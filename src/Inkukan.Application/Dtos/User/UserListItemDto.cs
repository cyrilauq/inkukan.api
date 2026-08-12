using Inkukan.Domain.Entities;

namespace Inkukan.Application.Dtos.User
{
    public class UserListItemDto
    {
        public Guid UserId { get; set; }
        public Guid SerieVolumeId { get; set; }
        public UserListType ListType { get; set; }
    }
}
