using FluentValidation;

namespace Inkukan.Application.Features.Type.Commands.Udpate
{
    public class UpdateCommandValidateor : AbstractValidator<UpdateTypeCommand>
    {
        public UpdateCommandValidateor()
        {
            RuleFor(t => t.Id)
                .NotNull().WithMessage("id_not_empty")
                .NotEmpty().WithMessage("id_not_empty");
            RuleFor(t => t.Name)
                .NotNull().WithMessage("name_not_empty")
                .NotEmpty().WithMessage("name_not_empty")
                .MinimumLength(3).WithMessage("name_min_length_3");
        }
    }
}
