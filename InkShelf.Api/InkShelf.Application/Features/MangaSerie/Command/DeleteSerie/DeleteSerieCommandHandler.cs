using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.MangaSerie.Command.DeleteSerie
{
    public class DeleteSerieCommandHandler(IMangaSerieRepository mangaSerieRepository)
        : BaseDeleteCommandHandler<Domain.Entities.MangaSerie, DeleteSerieCommand>(mangaSerieRepository)
    {
    }
}
