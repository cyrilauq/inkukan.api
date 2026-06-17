using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class UsersController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [AllowAnonymous]
        [HttpPost("{userId:guid}/lists/collection/volumes/{volumeId:guid}")]
        public Task<UserListItemDto> GetUserWishlistAsync(Guid userId, Guid volumeId, CancellationToken cancellationToken)
        {
            var command = new AddToUserCollectionCommand() 
            { 
                ListType = Domain.Entities.UserListType.Collection,
                SerieVolumeId = volumeId,
                UserId = userId,
            };

            return Mediator.Send(command, cancellationToken);
        }
    }
}
