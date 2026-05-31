using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Editor.Queries.GetAll
{
    public class GetAllEditorsQueryHandler(IEditorRepository editorRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.Editor, EditorDto, GetAllEditorsQuery>(editorRepository, mapper)
    {
    }
}
