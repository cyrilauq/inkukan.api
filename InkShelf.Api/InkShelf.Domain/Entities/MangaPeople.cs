using InkShelf.Domain.Entities.Interfaces;

namespace InkShelf.Domain.Entities
{
    public class MangaPeople : ITrackableEntity
    {
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;

        #region ITrackableEntity
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        #endregion
    }
}
