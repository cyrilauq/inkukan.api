using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class AuthController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost("register")]
        public Task<UserDto> CreateAsync([FromBody] RegisterCommand command)
            => Mediator.Send(command);
    }
}
