using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities;

public class UserListItem : ITrackableEntity, ILogicalDelete
{
    public UserListType Type { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid VolumeId { get; set; }
    public SerieVolume? Volume { get; set; }

    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
