using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class UsersController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpPost("lists")]
    [SwaggerResponse(StatusCodes.Status200OK, "The entry inside the user's collection")]
    [SwaggerOperation(Summary = "Add a volume inside user's collection")]
    public Task<UserListItemDto> GetUserWishlistAsync([Required][FromBody] AddToUserCollectionCommand command, CancellationToken cancellationToken) 
        => Mediator.Send(command, cancellationToken);
}
