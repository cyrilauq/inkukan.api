using FluentValidation;

namespace InkShelf.Application.Features.MangaPeople.Create
{
    public class CreateMangaPeopleValidator : AbstractValidator<CreateMangaPeopleCommand>
    {
        public CreateMangaPeopleValidator()
        {
            RuleFor(mp => mp.Lastname)
                .NotEmpty().WithMessage("lastname_empty")
                .NotNull().WithMessage("lastname_null")
                .MaximumLength(120).WithMessage("lastname_120_length");
            RuleFor(mp => mp.Firstname)
                .NotEmpty().WithMessage("firstname_empty")
                .NotNull().WithMessage("firstname_null")
                .MaximumLength(120).WithMessage("firstname_120_length");
        }
    }
}
