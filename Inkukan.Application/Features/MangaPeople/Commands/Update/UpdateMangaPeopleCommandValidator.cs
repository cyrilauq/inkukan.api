using FluentValidation;

namespace Inkukan.Application.Features.MangaPeople.Commands.Update
{
    public class UpdateMangaPeopleCommandValidator : AbstractValidator<UpdateMangaPeopleCommand>
    {
        public UpdateMangaPeopleCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("id_empty")
                .NotNull().WithMessage("id_empty")
                .NotEqual(Guid.Empty).WithMessage("id_empty");
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
