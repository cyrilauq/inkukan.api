using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<UserDto>
    {
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
