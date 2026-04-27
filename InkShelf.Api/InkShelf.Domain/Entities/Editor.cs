using InkShelf.Domain.Entities.Interfaces;

namespace InkShelf.Domain.Entities
{
    public class Editor : ITrackableEntity, ILogicalDelete
    {
        public string Name { get; set; } = null!;
        public DateTime ConstitutionDate { get; set; }
        public string Country { get; set; } = null!;
        public string? Website { get; set; }
        public string? ContactMail { get; set; }
        public string? Description { get; set; }

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
