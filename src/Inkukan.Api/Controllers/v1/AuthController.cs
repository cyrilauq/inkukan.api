using Asp.Versioning;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Auth.Commands.Login;
using Inkukan.Application.Features.Auth.Commands.Register;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1
{
    public class AuthController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost("register")]

        public async Task<UserDto> RegisterAsync([Required][FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            UserDto registerResult = await Mediator.Send(command, cancellationToken);

            return registerResult;
        }

        [HttpPost("login")]
        public async Task<UserDto> LoginAsync([Required][FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            UserDto loginResult = await Mediator.Send(command, cancellationToken);

            return loginResult;
        }

        [HttpPost("login2")]
        [MapToApiVersion("2.0")]
        public async Task<UserDto> Login2Async([Required][FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            UserDto loginResult = await Mediator.Send(command, cancellationToken);

            return loginResult;
        }
    }
}
