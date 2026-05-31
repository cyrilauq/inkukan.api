using FluentValidation;

namespace Inkukan.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("password_empty")
                .NotNull().WithMessage("password_null")
                .MinimumLength(10).WithMessage("password_length_10")
                .Must(password => password.Any(char.IsUpper)).WithMessage("password_should_have_uppercase")
                .Must(password => password.Any(char.IsLower)).WithMessage("password_should_have_lowercase")
                .Must(password => password.Any(char.IsNumber)).WithMessage("password_should_have_one_digit")
                .Must(password => password.Any(char.IsSymbol)).WithMessage("password_should_have_one_digit");
        }
    }
}
