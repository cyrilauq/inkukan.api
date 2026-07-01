using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class UsersController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpPost("lists")]
    public Task<UserListItemDto> GetUserWishlistAsync([Required][FromBody] AddToUserCollectionCommand command, CancellationToken cancellationToken) 
        => Mediator.Send(command, cancellationToken);
}
