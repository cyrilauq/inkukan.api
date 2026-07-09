using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Delete;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Application.Features.Type.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class TypeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried serie types")]
    [SwaggerOperation(Summary = "Get all serie types", Description = "Get all the serie types corresponding to the query inside a paginated result")]
    public Task<PaginatedDto<TypeDto>> GetAllAsync([Required][FromQuery] GetAllTypeQuery query, CancellationToken cancellationToken)
        => Mediator.Send(query, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "The created serie type")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a serie type")]
    public Task<TypeDto> CreateAsync([Required][FromBody] CreateTypeCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Delete a serie type")]
    public Task DeleteAsync([Required][FromRoute] Guid id, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteTypeCommand() { Id = id }, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The udpated serie type")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Udpate a serie type")]
    public Task<TypeDto> UpdateAsync([Required][FromRoute] Guid id, [Required][FromBody] UpdateTypeCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return Mediator.Send(command, cancellationToken);
    }
}
