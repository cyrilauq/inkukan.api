using InkShelf.Api.Controllers;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class TypeController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<IList<TypeDto>> GetAllAsync([FromQuery] GetAllTypeQuery query)
            => Mediator.Send(query);

        [HttpPost]
        public Task<TypeDto> CreateAsync([FromBody] CreateTypeCommand command)
            => Mediator.Send(command);
    }
}
