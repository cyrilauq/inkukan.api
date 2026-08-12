using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaCollection.Commands.Delete
{
    public class DeleteMangaCollectionCommandHandler(IBaseRepository<Domain.Entities.MangaCollection> mangaCollectionRepository) 
        : BaseDeleteCommandHandler<Domain.Entities.MangaCollection, DeleteMangaCollectionCommand>(mangaCollectionRepository)
    {
    }

    public class DeleteMangaCollectionCommand : BaseDeleteCommand
    {
    }
}
