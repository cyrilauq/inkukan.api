using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Interface;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using MediatR;

namespace Inkukan.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<RegisterCommand> validator)
        : IRequestHandler<RegisterCommand, UserDto>, IValidatable<RegisterCommand>
    {
        public async Task<bool> EnsureIsValidAsync(RegisterCommand value)
        {
            if ((await userRepository.GetByEmailAsync(value.Email) ?? await userRepository.GetByUsernameAsync(value.Username)) != null)
                throw new ConflictException($"A user with the email [{value.Email}] or username [{value.Username}] already exist");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);

            Domain.Entities.User addedUser = await userRepository.CreateAsync(mapper.Map<Domain.Entities.User>(request), request.Password, cancellationToken);

            return mapper.Map<UserDto>(addedUser);
        }
    }
}
