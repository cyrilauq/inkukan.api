using InkShelf.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Type.Commands.Delete
{
    public class DeleteTypeCommandHandler(ITypeRepository typeRepository) : BaseDeleteCommandHandler<InkShelf.Domain.Entities.MangaType, DeleteTypeCommand>(typeRepository)
    {
    }

    public class DeleteTypeCommand : BaseDeleteCommand
    {
    }
}
