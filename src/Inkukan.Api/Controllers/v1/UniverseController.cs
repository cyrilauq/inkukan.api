using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Universe.Commands.Create;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class UniverseController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK, "The created universe")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a new universe")]
    public Task<UniverseDto> CreateAsync([Required][FromBody] CreateUniverseCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);
}
