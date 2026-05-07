namespace InkShelf.Domain.Entities.Interfaces
{
    public interface ITrackableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
