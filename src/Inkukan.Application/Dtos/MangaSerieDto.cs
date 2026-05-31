using Inkukan.Domain.Entities;

namespace Inkukan.Application.Dtos
{
    public class MangaSerieDto
    {
        public Guid Id { get; set; }
        public required string TitleVF { get; set; }
        public required string TitleVO { get; set; }
        public required int TotalVolumes { get; set; }
        public required string Synopsis { get; set; }
        public Guid? AuthorId { get; set; }
        public MangaPeopleDto? Author { get; set; }
        public Guid? DrawerId { get; set; }
        public MangaPeopleDto? Drawer { get; set; }
        public Guid? TranslatorId { get; set; }
        public MangaPeopleDto? Translator { get; set; }
        public Guid? EditorVFId { get; set; }
        public EditorDto? EditorVF { get; set; }
        public Guid? EditorVOId { get; set; }
        public EditorDto? EditorVO { get; set; }
        public Guid? CollectionId { get; set; }
        public MangaCollectionDto? Collection { get; set; }
        public Guid? TypeId { get; set; }
        public MangaType? Type { get; set; }
        public IList<SerieVolumeDto> Volumes { get; set; } = [];
    }
}
