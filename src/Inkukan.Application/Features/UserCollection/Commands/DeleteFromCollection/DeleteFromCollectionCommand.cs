using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Features.UserCollection.Commands.DeleteFromCollection;

public class DeleteFromCollectionCommand : BaseDeleteCommand
{
    public Guid UserId { get; set; }
    public UserListType ListType { get; set; }
}
