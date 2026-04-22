using FluentValidation;

namespace InkShelf.Application.Features.MangaPeople.Update
{
    public class UpdateMangaPeopleCommandValidator : AbstractValidator<UpdateMangaPeopleCommand>
    {
        public UpdateMangaPeopleCommandValidator()
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
