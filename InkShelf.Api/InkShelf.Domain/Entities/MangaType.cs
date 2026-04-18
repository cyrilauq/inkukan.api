
using InkShelf.Domain.Entities.Interfaces;

namespace InkShelf.Domain.Entities
{
    public class MangaType : ITrackableEntity
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public IList<MangaSerie> Mangas { get; set; } = [];

        #region ITrackableEntity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion
    }
}
