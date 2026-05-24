using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities
{
    public class MangaPeople : ITrackableEntity, ILogicalDelete
    {
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;

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
