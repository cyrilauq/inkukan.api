using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.UserCollection.Commands.DeleteFromCollection;

public class DeleteFromCollectionCommandHandler(IUserlistItemRepository userlistItemRepository)
    : BaseDeleteCommandHandler<UserListItem, DeleteFromCollectionCommand>(userlistItemRepository)
{

    public async Task Handle(DeleteFromCollectionCommand request, CancellationToken cancellationToken)
    {
        UserListItem userListItem = await Repoditory.GetQuery()
            .Where(uli => 
                uli.UserId == request.UserId 
                && uli.VolumeId == request.Id
                && uli.Type == request.ListType)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("User doesn't have that tome in his collection");
        await Repoditory.DeleteAsync(userListItem, cancellationToken);
    }
}
