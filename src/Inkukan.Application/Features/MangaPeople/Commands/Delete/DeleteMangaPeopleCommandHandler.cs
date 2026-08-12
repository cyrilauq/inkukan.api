using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaPeople.Commands.Delete
{
    public class DeleteMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository)
        : BaseDeleteCommandHandler<Domain.Entities.MangaPeople, DeleteMangaPeopleCommand>(mangaPeopleRepository)
    {
    }
}
