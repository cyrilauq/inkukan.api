using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Editor.Commands.Delete
{
    public class DeleteEditorCommandHandler(IEditorRepository editorRepository)
        : BaseDeleteCommandHandler<Domain.Entities.Editor, DeleteEditorCommand>(editorRepository)
    {
    }
}
