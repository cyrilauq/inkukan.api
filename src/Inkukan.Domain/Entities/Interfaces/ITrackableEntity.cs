namespace Inkukan.Domain.Entities.Interfaces;

public interface ITrackableEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
