using FluentValidation;

namespace InkShelf.Application.Features.SerieVolume.Commands.Create
{
    public class CreateSerieVolumeCommandValidator : AbstractValidator<CreateSerieVolumeCommand>
    {
        public CreateSerieVolumeCommandValidator() 
        {
            RuleFor(c => c.VolumeNumber)
                .GreaterThanOrEqualTo(0).WithMessage("volumenumber_positif");
            RuleFor(c => c.Synopsis)
                .NotNull().WithMessage("synopsis_required")
                .NotEmpty().WithMessage("synopsis_required")
                .MinimumLength(15).WithMessage("synopsis_min_length_15")
                .MaximumLength(255).WithMessage("synopsis_max_length_15");
            RuleFor(c => c.VOParutionDate)
                .GreaterThan(DateTime.MinValue);
            RuleFor(c => c.VFParutionDate)
                .GreaterThan(DateTime.MinValue);
            RuleFor(c => c.VOParutionCountry)
                .NotEmpty().WithMessage("voparutioncountry_required")
                .NotNull().WithMessage("voparutioncountry_required")
                .MinimumLength(2).WithMessage("voparutioncountry_min_length_2")
                .MaximumLength(100).WithMessage("voparutioncountry_max_length_100");
            RuleFor(c => c.VFParutionCountry)
                .MinimumLength(2).WithMessage("vfparutioncountry_min_length_2")
                .MaximumLength(100).WithMessage("vfparutioncountry_max_length_100");
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
