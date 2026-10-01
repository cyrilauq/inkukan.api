using FluentValidation;

namespace Inkukan.Application.Features.Universe.Commands.Update;

public class UpdateUniverseValidator : AbstractValidator<UpdateUniverseCommand>
{
    public UpdateUniverseValidator()
    {
        RuleFor(universe => universe.Id)
            .NotEmpty().WithMessage("id_empty");
        RuleFor(universe => universe.Name)
            .NotEmpty().WithMessage("name_empty")
            .MaximumLength(100).WithMessage("name_100_length");
    }
}
