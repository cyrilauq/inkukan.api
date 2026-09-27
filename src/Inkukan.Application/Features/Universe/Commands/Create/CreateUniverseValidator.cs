using FluentValidation;

namespace Inkukan.Application.Features.Universe.Commands.Create;

public class CreateUniverseValidator : AbstractValidator<CreateUniverseCommand>
{
    public CreateUniverseValidator()
    {
        RuleFor(universe => universe.Name)
            .NotEmpty().WithMessage("name_empty")
            .MaximumLength(100).WithMessage("name_100_length");
        RuleFor(universe => universe.Code)
            .NotEmpty().WithMessage("code_empty")
            .MaximumLength(50).WithMessage("code_50_length");
    }
}
