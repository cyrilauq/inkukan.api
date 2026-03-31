
namespace InkShelf.Domain.Entities
{
    public class MangaType : ITrackableEntity
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public IList<MangaSerie> Mangas { get; set; } = [];
    }
}
