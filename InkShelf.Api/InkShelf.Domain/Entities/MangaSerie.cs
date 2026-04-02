namespace InkShelf.Domain.Entities
{
    public class MangaSerie : ITrackableEntity
    {
        // TODO : add something to "save" the cover
        public Guid Id { get; set; }
        
        public string TitleVF { get; set; } = null!;
        public string TitleVO { get; set; } = null!;

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
    }
}
