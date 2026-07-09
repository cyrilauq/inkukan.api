using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Auth.Commands.Login;
using Inkukan.Application.Features.Auth.Commands.Register;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

[AllowAnonymous]
public class AuthController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpPost("register")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(UserDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "One or more validation errors occured")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "A user with the same email/username already exists")]
    [SwaggerOperation(Summary = "Create a user", Description = "Let a user create an account on the plateform")]
    public async Task<UserDto> RegisterAsync([Required][FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        UserDto registerResult = await Mediator.Send(command, cancellationToken);

        return registerResult;
    }

    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successfull", typeof(UserDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "One or more validation errors occured")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "If user credentials are invalid")]
    [SwaggerOperation(Summary = "Get a user", Description = "Get the user account and its access token based on his credentials")]
    public async Task<UserDto> LoginAsync([Required][FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        UserDto loginResult = await Mediator.Send(command, cancellationToken);

        return loginResult;
    }
}
