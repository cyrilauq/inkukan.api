using Inkukan.Application.Dtos.User;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection
{
    public class AddToUserCollectionCommand : IRequest<UserListItemDto>
    {
        public Guid UserId { get; set; }
        public Guid SerieVolumeId { get; set; }
        public UserListType ListType { get; set; }
    }
}
