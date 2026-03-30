
namespace InkShelf.Domain.Entities
{
    public class MangaCollection : ITrackableEntity
    {
        public Guid Id {  get; set; }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public IList<MangaSerie> Manga { get; set; } = [];
    }
}
