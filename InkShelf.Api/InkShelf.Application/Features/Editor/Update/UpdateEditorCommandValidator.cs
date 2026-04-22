using FluentValidation;

namespace InkShelf.Application.Features.Editor.Update
{
    public class UpdateEditorCommandValidator : AbstractValidator<UpdateEditorCommand>
    {
        public UpdateEditorCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("id_empty")
                .NotNull().WithMessage("id_empty")
                .NotEqual(Guid.Empty).WithMessage("id_empty");
            RuleFor(e => e.Name)
                .NotNull().WithMessage("name_null")
                .NotEmpty().WithMessage("name_empty")
                .MaximumLength(100).WithMessage("name_100_length");
            RuleFor(e => e.Description)
                .MaximumLength(255).WithMessage("description_max_255_length");
            RuleFor(e => e.Country)
                .NotNull().WithMessage("country_null")
                .NotEmpty().WithMessage("country_empty")
                .MinimumLength(2).WithMessage("country_min_2_length")
                .MaximumLength(100).WithMessage("country_max_100_length");
            RuleFor(e => e.ConstitutionDate)
                .NotNull().WithMessage("constitution_date_null")
                .NotEmpty().WithMessage("constitution_date_empty")
                .GreaterThan(DateTime.MinValue).WithMessage("constitution_date_min");
        }
    }
}
