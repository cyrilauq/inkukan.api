using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities
{
    public class MangaType : ITrackableEntity, ILogicalDelete
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public IList<MangaSerie> Mangas { get; set; } = [];

        #region ITrackableEntity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion

        #region ILogicalDelete
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; }
        #endregion
    }
}
