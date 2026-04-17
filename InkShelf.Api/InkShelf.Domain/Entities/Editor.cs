namespace InkShelf.Domain.Entities
{
    public class Editor : ITrackableEntity
    {
        public Guid Id { get; }

        public string Name { get; set; } = null!;
        public DateTime ConstitutionDate { get; set; }
        public string Country { get; set; } = null!;
        public string? Website { get; set; }
        public string? ContactMail { get; set; }
        public string? Description { get; set; }
    }
}
