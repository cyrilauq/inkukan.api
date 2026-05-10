using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Auth.Commands.Login;
using InkShelf.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
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
