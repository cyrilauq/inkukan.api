using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Inkukan.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
[SwaggerResponse(StatusCodes.Status401Unauthorized, "If the user is unauthenticated")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "One or more validation errors occured")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ApplicationBaseController(IInkukaMediator mediator) : ControllerBase
{
    protected IInkukaMediator Mediator { get; init; } = mediator;
}
