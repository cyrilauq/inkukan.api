using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Delete;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Application.Features.Type.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    public class TypeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<PaginatedDto<TypeDto>> GetAllAsync([Required][FromQuery] GetAllTypeQuery query)
            => Mediator.Send(query);

        [HttpPost]
        public Task<TypeDto> CreateAsync([Required][FromBody] CreateTypeCommand command)
            => Mediator.Send(command);

        [HttpDelete("{id:guid}")]
        public Task DeleteAsync([Required] Guid id)
            => Mediator.Send(new DeleteTypeCommand() { Id = id });

        [HttpPut("{id:guid}")]
        public Task<TypeDto> UpdateAsync([Required] Guid id, [Required][FromBody] UpdateTypeCommand command)
        {
            command.Id = id;
            return Mediator.Send(command);
        }
    }
}
