using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Delete;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Application.Features.Type.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class TypeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<PaginatedDto<TypeDto>> GetAllAsync([FromQuery] GetAllTypeQuery query)
            => Mediator.Send(query);

        [HttpPost]
        public Task<TypeDto> CreateAsync([FromBody] CreateTypeCommand command)
            => Mediator.Send(command);

        [HttpDelete("{id:guid}")]
        public Task DeleteAsync(Guid id)
            => Mediator.Send(new DeleteTypeCommand() { Id = id });

        [HttpPut("{id:guid}")]
        public Task<TypeDto> UpdateAsync(Guid id, [FromBody] UpdateTypeCommand command)
        {
            command.Id = id;
            return Mediator.Send(command);
        }
    }
}
