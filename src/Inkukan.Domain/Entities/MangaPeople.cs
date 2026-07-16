using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities;

public class MangaPeople : ITrackableEntity, ILogicalDelete
{
    public string Lastname { get; set; } = null!;
    public string Firstname { get; set; } = null!;

    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; }
}
