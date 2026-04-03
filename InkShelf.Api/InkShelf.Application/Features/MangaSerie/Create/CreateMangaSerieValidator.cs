using FluentValidation;

namespace InkShelf.Application.Features.MangaSerie.Create
{
    public class CreateMangaSerieValidator : AbstractValidator<CreateMangaSerieCommand>
    {
        public CreateMangaSerieValidator()
        {
            RuleFor(m => m.TitleVF)
                .NotEmpty().WithMessage("titlevf_empty")
                .NotNull().WithMessage("titlevf_null")
                .MaximumLength(120).WithMessage("titlevf_120_length");
            RuleFor(m => m.TitleVO)
                .NotEmpty().WithMessage("titlevo_empty")
                .NotNull().WithMessage("titlevo_null")
                .MaximumLength(120).WithMessage("titlevo_120_length");
            RuleFor(m => m.TotalVolumes)
                .GreaterThanOrEqualTo(0);
        }
    }
}
