namespace InkShelf.Domain.Entities
{
    public class Editor : ITrackableEntity
    {
        public Guid Id { get; }

        public string Name { get; set; } = null!;

        public IList<MangaSerie> Manga { get; set; } = [];
    }
}
