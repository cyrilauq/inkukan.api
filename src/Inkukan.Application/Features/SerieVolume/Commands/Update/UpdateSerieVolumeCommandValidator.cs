using FluentValidation;

namespace Inkukan.Application.Features.SerieVolume.Commands.Update
{
    public class UpdateSerieVolumeCommandValidator : AbstractValidator<UpdateSerieVolumeCommand>
    {
        public UpdateSerieVolumeCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("id_empty")
                .NotNull().WithMessage("id_empty")
                .NotEqual(Guid.Empty).WithMessage("id_empty");
            RuleFor(c => c.VolumeNumber)
                .GreaterThanOrEqualTo(0).WithMessage("volumenumber_positif");
            RuleFor(c => c.Synopsis)
                .NotNull().WithMessage("synopsis_required")
                .NotEmpty().WithMessage("synopsis_required")
                .MinimumLength(15).WithMessage("synopsis_min_length_15")
                .MaximumLength(1000).WithMessage("synopsis_max_length_1000");
            RuleFor(c => c.VOParutionDate)
                .GreaterThan(DateTime.MinValue);
            RuleFor(c => c.VFParutionDate)
                .GreaterThan(DateTime.MinValue);
            RuleFor(c => c.EANCode)
                .MaximumLength(25).WithMessage("eancode_max_length_25");
            RuleFor(c => c.PriceCode)
                .MaximumLength(25).WithMessage("pricecode_max_length_25");
            RuleFor(c => c.MangaSerieId)
                .NotEmpty().WithMessage("mangaserieid_required")
                .NotNull().WithMessage("mangaserieid_required")
                .NotEqual(Guid.Empty).WithMessage("mangaserieid_required");
        }
    }
}
