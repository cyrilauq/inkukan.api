using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.Editor.Queries.GetAll
{
    public class GetAllEditorsQueryHandler(IEditorRepository editorRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.Editor, EditorDto, GetAllEditorsQuery>(editorRepository, mapper)
    {
    }
}
