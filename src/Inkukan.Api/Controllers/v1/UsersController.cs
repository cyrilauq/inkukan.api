using Inkukan.Application.Dtos;
using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Application.Features.UserCollection.Commands.DeleteFromCollection;
using Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionByName;
using Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionSeriesByType;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities;
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
    
    [HttpPost("{userId:guid}/lists/{listName}/volumes")]
    [SwaggerResponse(StatusCodes.Status200OK, "The entry inside the user's collection")]
    [SwaggerOperation(Summary = "Add a volume inside user's collection")]
    public Task<PaginatedDto<SerieVolumeDto>> GetUserListVolumesAsync([Required] Guid userId, [Required] UserListType listName, [FromQuery][Required] int pageSize, [FromQuery][Required] int pageNumber, CancellationToken cancellationToken)
        => Mediator.Send(new GetUserCollectionVolumesByTypeQuery() { CollectionName = listName, UserId = userId, PageSize = pageSize, PageNumber = pageNumber }, cancellationToken);

    [HttpPost("{userId:guid}/lists/{listName}/series")]
    [SwaggerResponse(StatusCodes.Status200OK, "The entry inside the user's collection")]
    [SwaggerOperation(Summary = "Add a volume inside user's collection")]
    public Task<PaginatedDto<SerieListDto>> GetUserListSeriesAsync([Required] Guid userId, [Required] UserListType listName, [FromQuery][Required] int pageSize, [FromQuery][Required] int pageNumber, CancellationToken cancellationToken)
        => Mediator.Send(new GetUserCollectionSeriesByTypeQuery() { CollectionName = listName, UserId = userId, PageSize = pageSize, PageNumber = pageNumber }, cancellationToken);

    [HttpDelete("{userId:guid}/lists/{listName}/volumes/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "")]
    [SwaggerOperation(Summary = "Delete a collection item from user's lists")]
    public Task DeleteFromCollectionAsync([Required][FromRoute] Guid id, [Required][FromRoute] UserListType listName, [Required][FromRoute] Guid userId, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteFromCollectionCommand() { Id = id, ListType = listName, UserId = userId }, cancellationToken);
}
