namespace Inkukan.Domain.Entities.Interfaces;

public interface ILogicalDelete
{
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; }
}
