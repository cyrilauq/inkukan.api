using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Auth.Commands.Login;
using Inkukan.Application.Features.Auth.Commands.Register;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    public class AuthController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost("register")]
        public async Task<UserDto> RegisterAsync([Required][FromBody] RegisterCommand command)
        {
            UserDto registerResult = await Mediator.Send(command);

            return registerResult;
        }

        [HttpPost("login")]
        public async Task<UserDto> LoginAsync([Required][FromBody] LoginCommand command)
        {
            UserDto loginResult = await Mediator.Send(command);

            return loginResult;
        }
    }
}
