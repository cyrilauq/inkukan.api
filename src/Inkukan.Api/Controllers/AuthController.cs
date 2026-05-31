using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Auth.Commands.Login;
using Inkukan.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class AuthController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost("register")]
        public async Task<UserDto> RegisterAsync([FromBody] RegisterCommand command)
        {
            UserDto registerResult = await Mediator.Send(command);

            return registerResult;
        }

        [HttpPost("login")]
        public async Task<UserDto> LoginAsync([FromBody] LoginCommand command)
        {
            UserDto loginResult = await Mediator.Send(command);

            return loginResult;
        }
    }
}
