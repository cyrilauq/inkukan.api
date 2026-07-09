using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Inkukan.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
[SwaggerResponse(StatusCodes.Status401Unauthorized, "If the user is unauthenticated")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "One or more validation errors occured")]
public class ApplicationBaseController(IInkukaMediator mediator) : ControllerBase
{
    protected IInkukaMediator Mediator { get; init; } = mediator;
}
