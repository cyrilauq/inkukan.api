using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.Editor.Commands.Delete
{
    public class DeleteMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository)
        : BaseDeleteCommandHandler<Domain.Entities.MangaPeople, DeleteMangaPeopleCommand>(mangaPeopleRepository)
    {
    }
}
