using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities
{
    public class UserListItem : ITrackableEntity
    {
        public UserListType Type { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid VolumeId { get; set; }
        public SerieVolume? Volume { get; set; }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
