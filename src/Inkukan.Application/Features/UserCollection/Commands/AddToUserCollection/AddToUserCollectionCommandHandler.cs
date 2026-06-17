using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection
{
    public class AddToUserCollectionCommandHandler(IBaseRepository<UserListItem> baseRepository, IValidator<AddToUserCollectionCommand> validator, IMapper mapper)
        : BaseCreateCommandHandler<AddToUserCollectionCommand, UserListItemDto, UserListItem>(baseRepository, validator, mapper)
    {
    }
}
