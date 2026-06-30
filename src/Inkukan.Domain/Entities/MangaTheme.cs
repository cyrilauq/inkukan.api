using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities
{
    public class MangaTheme : ITrackableEntity, ILogicalDelete
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public IList<MangaSerie> Mangas { get; set; } = [];

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; }
    }
}
