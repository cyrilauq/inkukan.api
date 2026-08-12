using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Type.Commands.Delete
{
    public class DeleteTypeCommandHandler(ITypeRepository typeRepository) : BaseDeleteCommandHandler<MangaType, DeleteTypeCommand>(typeRepository)
    {
    }

    public class DeleteTypeCommand : BaseDeleteCommand
    {
    }
}
