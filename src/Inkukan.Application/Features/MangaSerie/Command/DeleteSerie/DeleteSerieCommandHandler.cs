using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaSerie.Command.DeleteSerie
{
    public class DeleteSerieCommandHandler(IMangaSerieRepository mangaSerieRepository)
        : BaseDeleteCommandHandler<Domain.Entities.MangaSerie, DeleteSerieCommand>(mangaSerieRepository)
    {
    }
}
