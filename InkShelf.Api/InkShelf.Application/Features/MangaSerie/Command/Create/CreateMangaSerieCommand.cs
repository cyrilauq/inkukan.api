using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.MangaSerie.Command.Create
{
    public class CreateMangaSerieCommand : IRequest<MangaSerieDto>
    {
        public Guid Id { get; set; }
        public string TitleVF { get; set; } = string.Empty;
        public string TitleVO { get; set; } = string.Empty;
        public int TotalVolumes { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public Guid? AuthorId { get; set; }
        public Guid? DrawerId { get; set; }
        public Guid? TranslatorId { get; set; }
        public Guid? EditorVFId { get; set; }
        public Guid? EditorVOId { get; set; }
        public Guid? CollectionId { get; set; }
        public Guid? TypeId { get; set; }
    }
}
