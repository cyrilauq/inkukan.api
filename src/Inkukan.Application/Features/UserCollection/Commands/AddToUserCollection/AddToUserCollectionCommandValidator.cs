using FluentValidation;

namespace Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection
{
    public class AddToUserCollectionCommandValidator : AbstractValidator<AddToUserCollectionCommand>
    {
        // TODO : add some tests
        public AddToUserCollectionCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("user_empty")
                .NotNull().WithMessage("user_empty");
            RuleFor(c => c.SerieVolumeId)
                .NotEmpty().WithMessage("volume_empty")
                .NotNull().WithMessage("volume_empty");
        }
    }
}
