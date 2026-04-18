namespace InkShelf.Domain.Entities.Interfaces
{
    public interface ILogicalDelete
    {
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; }
    }
}
