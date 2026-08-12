using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<UserDto>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
