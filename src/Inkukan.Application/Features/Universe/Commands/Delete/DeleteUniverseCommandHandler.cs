using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Delete;

public class DeleteUniverseCommandHandler(IUniverseRepository universeRepository)
    : BaseDeleteCommandHandler<UniverseEntity, DeleteUniverseCommand>(universeRepository)
{
    public override Task DeleteAsync(DeleteUniverseCommand request, UniverseEntity entity, CancellationToken cancellationToken)
        => universeRepository.DeleteWithSeriesAsync(entity, cancellationToken);
}
