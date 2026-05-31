using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaCollection.Commands.Create;
using Inkukan.Application.Features.MangaCollection.Commands.Delete;
using Inkukan.Application.Features.MangaCollection.Commands.Update;
using Inkukan.Application.Features.MangaCollection.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class CollectionController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<PaginatedDto<MangaCollectionDto>> GetAllAsync([FromQuery] GetAllCollectionQuery query)
            => Mediator.Send(query);

        [HttpPost]
        public Task<MangaCollectionDto> CreateAsync([FromBody] CreateMangaCollectionCommand command)
            => Mediator.Send(command);

        [HttpPut("{id:guid}")]
        public Task<MangaCollectionDto> UpdateAsync(Guid id, [FromBody] UpdateMangaCollectionCommand command)
        {
            command.Id = id;
            return Mediator.Send(command);
        }

        [HttpDelete("{id:guid}")]
        public Task DeleteAsync(Guid id)
            => Mediator.Send(new DeleteMangaCollectionCommand() { Id = id });
    }
}
