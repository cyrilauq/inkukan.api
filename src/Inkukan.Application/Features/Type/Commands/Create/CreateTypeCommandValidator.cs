using FluentValidation;

namespace Inkukan.Application.Features.Type.Commands.Create
{
    public class CreateTypeCommandValidator : AbstractValidator<CreateTypeCommand>
    {
        public CreateTypeCommandValidator() 
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage("name_not_empty")
                .NotEmpty().WithMessage("name_not_empty")
                .MinimumLength(3).WithMessage("name_min_length_3");
        }
    }
}
