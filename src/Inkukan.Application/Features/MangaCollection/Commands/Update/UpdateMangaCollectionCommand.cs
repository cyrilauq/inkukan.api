using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.MangaCollection.Commands.Update
{
    public class UpdateMangaCollectionCommand : IRequest<MangaCollectionDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
