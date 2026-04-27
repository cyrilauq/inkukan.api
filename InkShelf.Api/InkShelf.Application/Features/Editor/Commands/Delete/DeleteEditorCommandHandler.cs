using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.Editor.Commands.Delete
{
    public class DeleteEditorCommandHandler(IEditorRepository editorRepository)
        : BaseDeleteCommandHandler<Domain.Entities.Editor, DeleteEditorCommand>(editorRepository)
    {
    }
}
