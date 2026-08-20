using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions.Query;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionByName;

public class GetUserCollectionVolumesByTypeQuery : PaginatedQueryBase, IRequest<PaginatedDto<SerieVolumeDto>>
{
    public required Guid UserId { get; set; }
    public required UserListType CollectionName { get; set; }
}
