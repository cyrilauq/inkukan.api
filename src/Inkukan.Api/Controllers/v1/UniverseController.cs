using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Universe.Commands.Create;
using Inkukan.Application.Features.Universe.Commands.Update;
using Inkukan.Application.Features.Universe.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class UniverseController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried universes")]
    [SwaggerOperation(Summary = "Get all universes", Description = "Get all universes corresponding to the query inside a paginated result")]
    public Task<PaginatedDto<UniverseDto>> GetAllAsync([Required][FromQuery] GetAllUniversesQuery query, CancellationToken cancellationToken)
        => Mediator.Send(query, cancellationToken);

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK, "The created universe")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a new universe")]
    public Task<UniverseDto> CreateAsync([Required][FromBody] CreateUniverseCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK, "The updated universe")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Update a universe")]
    public Task<UniverseDto> UpdateAsync([Required][FromRoute] Guid id, [Required][FromBody] UpdateUniverseCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return Mediator.Send(command, cancellationToken);
    }
}
