using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Editor.Create;
using InkShelf.Application.Features.Editor.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class EditorController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<EditorDto> CreateAsync(CreateEditorCommand command)
            => Mediator.Send(command);

        [HttpPut("{editorId:guid}")]
        public Task<EditorDto> UpdateAsync(Guid editorId, [FromBody] UpdateEditorCommand command)
        {
            command.Id = editorId;
            return Mediator.Send(command);
        }
    }
}
