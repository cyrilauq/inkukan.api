using InkShelf.Application.Dtos;
using InkShelf.Application.Interface;
using MediatR;

namespace InkShelf.Application.Features.Auth.Register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, UserDto>, IValidatable<UserDto>
    {
        public Task<bool> EnsureIsValidAsync(UserDto value)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
