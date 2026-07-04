using Inkukan.Domain.Entities.Interfaces;

namespace Inkukan.Domain.Entities
{
    public class MangaSerie : ITrackableEntity, ILogicalDelete
    {
        public string TitleVF { get; set; } = null!;
        public string TitleVO { get; set; } = null!;

        public string? VFParutionCountry { get; set; }
        public string VOParutionCountry { get; set; } = null!;

        public int TotalVolumes { get; set; }
        public string Synopsis { get; set; } = null!;

        public MangaPeople? Author { get; set; }
        public Guid? AuthorId { get; set; }

        public MangaPeople? Drawer { get; set; }
        public Guid? DrawerId { get; set; }

        public MangaPeople? Translator { get; set; }
        public Guid? TranslatorId { get; set; }

        public Editor? EditorVF { get; set; }
        public Guid? EditorVFId { get; set; }

        public Editor? EditorVO { get; set; }
        public Guid? EditorVOId { get; set; }

        public MangaCollection Collection { get; set; } = null!;
        public Guid CollectionId { get; set; }

        public MangaType Type { get; set; } = null!;
        public Guid TypeId { get; set; }

        public IList<MangaTheme> Themes { get; set; } = [];
        public IList<SerieVolume> Volumes { get; set; } = [];

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; }
    }
}
