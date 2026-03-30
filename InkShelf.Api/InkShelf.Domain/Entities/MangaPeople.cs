namespace InkShelf.Domain.Entities
{
    public class MangaPeople : ITrackableEntity
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;

        public IList<MangaSerie> Mangas { get; set; } = [];
    }
}
