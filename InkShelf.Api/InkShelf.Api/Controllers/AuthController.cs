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
        public Task<UserDto> RegisterAsync([FromBody] RegisterCommand command)
            => Mediator.Send(command);

        [HttpPost("login")]
        public Task<UserDto> LoginAsync([FromBody] LoginCommand command)
            => Mediator.Send(command);
    }
}
