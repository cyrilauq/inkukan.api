using FluentValidation;

namespace Inkukan.Application.Features.MangaCollection.Commands.Create
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
