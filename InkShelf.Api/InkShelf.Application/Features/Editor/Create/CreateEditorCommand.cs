using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.Editor.Create
{
    public class CreateEditorCommand : IRequest<EditorDto>
    {
        public string Name { get; set; } = string.Empty;
    }
}
