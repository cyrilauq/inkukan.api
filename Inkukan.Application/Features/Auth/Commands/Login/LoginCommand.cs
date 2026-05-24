using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<UserDto>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
