using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<UserDto>
    {
    }
}
