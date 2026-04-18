namespace InkShelf.Application.Dtos
{
    public class MangaSerieDto
    {
        public Guid Id { get; set; }
        public required string TitleVF { get; set; }
        public required string TitleVO { get; set; }
        public required int TotalVolumes { get; set; }
        public required string Synopsis { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? DrawerId { get; set; }
        public Guid? TranslatorId { get; set; }
        public Guid? EditorVFId { get; set; }
        public Guid? EditorVOId { get; set; }
        public Guid? CollectionId { get; set; }
        public Guid? TypeId { get; set; }
        public IList<SerieVolumeDto> Volumes { get; set; } = [];
    }
}
