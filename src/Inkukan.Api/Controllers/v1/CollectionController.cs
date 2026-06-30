using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaCollection.Commands.Create;
using Inkukan.Application.Features.MangaCollection.Commands.Delete;
using Inkukan.Application.Features.MangaCollection.Commands.Update;
using Inkukan.Application.Features.MangaCollection.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1
{
    public class CollectionController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<PaginatedDto<MangaCollectionDto>> GetAllAsync([Required][FromQuery] GetAllCollectionQuery query, CancellationToken cancellationToken)
            => Mediator.Send(query, cancellationToken);

        [HttpPost]
        public Task<MangaCollectionDto> CreateAsync([Required][FromBody] CreateMangaCollectionCommand command, CancellationToken cancellationToken)
            => Mediator.Send(command, cancellationToken);

        [HttpPut("{id:guid}")]
        public Task<MangaCollectionDto> UpdateAsync([Required] Guid id, [Required][FromBody] UpdateMangaCollectionCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id:guid}")]
        public Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken)
            => Mediator.Send(new DeleteMangaCollectionCommand() { Id = id }, cancellationToken);
    }
}
