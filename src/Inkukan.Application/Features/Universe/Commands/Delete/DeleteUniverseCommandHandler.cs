using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Delete;

public class DeleteUniverseCommandHandler(IBaseRepository<UniverseEntity> universeRepository)
    : BaseDeleteCommandHandler<UniverseEntity, DeleteUniverseCommand>(universeRepository)
{
}
