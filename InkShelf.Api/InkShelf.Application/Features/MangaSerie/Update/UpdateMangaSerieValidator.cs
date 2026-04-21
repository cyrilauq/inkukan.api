using FluentValidation;
using InkShelf.Application.Features.MangaSerie.Create;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Features.MangaSerie.Update
{
    public class UpdateMangaSerieValidator : AbstractValidator<UpdateMangaSerieCommand>
    {
        public UpdateMangaSerieValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("id_empty")
                .NotNull().WithMessage("id_empty")
                .NotEqual(Guid.Empty).WithMessage("id_empty");
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
