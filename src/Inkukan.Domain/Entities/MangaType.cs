using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities;

public class MangaType : ITrackableEntity, ILogicalDelete
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public IList<MangaSerie> Mangas { get; set; } = [];

    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; }
}
