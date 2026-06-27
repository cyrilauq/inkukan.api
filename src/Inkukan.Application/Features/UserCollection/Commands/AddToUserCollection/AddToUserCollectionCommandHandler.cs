using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection
{
    public class AddToUserCollectionCommandHandler(IBaseRepository<UserListItem> baseRepository, IBaseRepository<User> userRepository, IBaseRepository<Domain.Entities.SerieVolume> volumeRepository, IValidator<AddToUserCollectionCommand> validator, IMapper mapper)
        : BaseCreateCommandHandler<AddToUserCollectionCommand, UserListItemDto, UserListItem>(baseRepository, validator, mapper)
    { 
        public override async Task<bool> EnsureIsValidAsync(AddToUserCollectionCommand value, CancellationToken cancellationToken = default)
        {
            bool baseResult = await base.EnsureIsValidAsync(value, cancellationToken);

            if (await volumeRepository.GetByIdAsync(value.SerieVolumeId, cancellationToken) is null)
                throw new EntityNotFoundException("No volume with the provided id were found");
            if (await userRepository.GetByIdAsync(value.UserId, cancellationToken) is null)
                throw new EntityNotFoundException("No user with the provided id were found");

            return baseResult;
        }
    }
}
