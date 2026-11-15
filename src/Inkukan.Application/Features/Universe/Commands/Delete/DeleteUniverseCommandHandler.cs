using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MangaSerieEntity = Inkukan.Domain.Entities.MangaSerie;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Delete;

public class DeleteUniverseCommandHandler(IBaseRepository<UniverseEntity> universeRepository, IBaseRepository<MangaSerieEntity> mangaSerieRepository)
    : BaseDeleteCommandHandler<UniverseEntity, DeleteUniverseCommand>(universeRepository)
{
    public override async Task BeforeDeleteAsync(DeleteUniverseCommand request, UniverseEntity entity, CancellationToken cancellationToken)
    {
        List<MangaSerieEntity> series = await mangaSerieRepository.GetQuery()
            .AsNoTracking()
            .Where(serie => serie.UniverseId == entity.Id)
            .ToListAsync(cancellationToken);

        foreach (MangaSerieEntity serie in series)
            await mangaSerieRepository.DeleteAsync(serie, cancellationToken);
    }
}
