using FluentValidation;

namespace Inkukan.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("password_empty")
                .NotNull().WithMessage("password_null")
                .MinimumLength(10).WithMessage("password_length_10")
                .Must(password => password.Any(char.IsUpper)).WithMessage("password_should_have_uppercase")
                .Must(password => password.Any(char.IsLower)).WithMessage("password_should_have_lowercase")
                .Must(password => password.Any(char.IsNumber)).WithMessage("password_should_have_one_digit")
                .Must(password => password.Any(char.IsSymbol)).WithMessage("password_should_have_one_digit");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("email_empty")
                .NotNull().WithMessage("email_null");
            RuleFor(c => c.Firstname)
                .NotEmpty().WithMessage("firstname_empty")
                .NotNull().WithMessage("firstname_null");
            RuleFor(c => c.Lastname)
                .NotEmpty().WithMessage("lastname_empty")
                .NotNull().WithMessage("lastname_null");
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("username_empty")
                .NotNull().WithMessage("username_null");
        }
    }
}
