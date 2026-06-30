using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Delete;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Application.Features.Type.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1
{
    public class TypeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpGet]
        public Task<PaginatedDto<TypeDto>> GetAllAsync([Required][FromQuery] GetAllTypeQuery query, CancellationToken cancellationToken)
            => Mediator.Send(query, cancellationToken);

        [HttpPost]
        public Task<TypeDto> CreateAsync([Required][FromBody] CreateTypeCommand command, CancellationToken cancellationToken)
            => Mediator.Send(command, cancellationToken);

        [HttpDelete("{id:guid}")]
        public Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken)
            => Mediator.Send(new DeleteTypeCommand() { Id = id }, cancellationToken);

        [HttpPut("{id:guid}")]
        public Task<TypeDto> UpdateAsync([Required] Guid id, [Required][FromBody] UpdateTypeCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return Mediator.Send(command, cancellationToken);
        }
    }
}
