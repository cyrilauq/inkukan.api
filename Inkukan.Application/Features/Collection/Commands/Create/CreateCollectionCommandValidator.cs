using FluentValidation;
using Inkukan.Application.Features.Collection.Commands.Create;

namespace Inkukan.Application.Features.Type.Commands.Create
{
    public class CreateCollectionCommandValidator : AbstractValidator<CreateMangaCollectionCommand>
    {
        public CreateCollectionCommandValidator() 
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage("name_not_empty")
                .NotEmpty().WithMessage("name_not_empty")
                .MinimumLength(3).WithMessage("name_min_length_3");
        }
    }
}
