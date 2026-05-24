using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.MangaCollection.Commands.Update
{
    public class UpdateMangaCollectionCommand : IRequest<MangaCollectionDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
