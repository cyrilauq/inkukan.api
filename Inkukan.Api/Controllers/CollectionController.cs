using InkShelf.Api.Controllers;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Collection.Commands.Create;
using Inkukan.Application.Features.Type.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class CollectionController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<IList<CollectionDto>> GetAllAsync([FromQuery] GetAllCollectionQuery query)
            => Mediator.Send(query);

        [HttpPost]
        public Task<CollectionDto> CreateAsync([FromBody] CreateCollectionCommand command)
            => Mediator.Send(command);
    }
}
