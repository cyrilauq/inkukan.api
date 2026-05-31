using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Interface;
using Inkukan.Application.Services;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using MediatR;

namespace Inkukan.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper, IValidator<LoginCommand> validator)
        : IRequestHandler<LoginCommand, UserDto>, IValidatable<LoginCommand>
    {
        public async Task<bool> EnsureIsValidAsync(LoginCommand value)
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid)
                return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);

            Domain.Entities.User? userToLogin = await userRepository.FindByCredentials(request.Login, request.Password, cancellationToken);
            if (userToLogin == null)
                throw new WrongCredentialsException("Wrong credentials");
            UserDto user = mapper.Map<UserDto>(userToLogin);
            user.AccessToken = await tokenService.GetTokenForUserAsync(userToLogin);
            return user;
        }
    }
}
