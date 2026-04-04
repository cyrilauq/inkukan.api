using FluentValidation;

namespace InkShelf.Application.Features.Editor.Create
{
    public class CreateEditorValidator : AbstractValidator<CreateEditorCommand>
    {
        public CreateEditorValidator()
        {
            RuleFor(e => e.Name)
                .NotNull().WithMessage("name_null")
                .NotEmpty().WithMessage("name_empty")
                .MaximumLength(100).WithMessage("name_100_length");
        }
    }
}
