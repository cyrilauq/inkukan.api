using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Editor.Commands.Create;
using InkShelf.Application.Features.Editor.Commands.Update;
using InkShelf.Application.Features.Editor.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class EditorController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<EditorDto> CreateAsync(CreateEditorCommand command)
            => Mediator.Send(command);

        [HttpGet]
        public Task<IList<EditorDto>> GetAllAsync([FromQuery] GetAllEditorsQuery command)
            => Mediator.Send(command);

        [HttpPut("{editorId:guid}")]
        public Task<EditorDto> UpdateAsync(Guid editorId, [FromBody] UpdateEditorCommand command)
        {
            command.Id = editorId;
            return Mediator.Send(command);
        }
    }
}
