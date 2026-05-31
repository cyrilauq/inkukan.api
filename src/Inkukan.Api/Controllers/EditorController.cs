using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Editor.Commands.Create;
using Inkukan.Application.Features.Editor.Commands.Delete;
using Inkukan.Application.Features.Editor.Commands.Update;
using Inkukan.Application.Features.Editor.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    public class EditorController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<EditorDto> CreateAsync([Required] CreateEditorCommand command)
            => Mediator.Send(command);

        [HttpDelete("{editorId:guid}")]
        public Task DeleteAsync([Required] Guid editorId)
            => Mediator.Send(new DeleteEditorCommand { Id = editorId });

        [HttpGet]
        public Task<PaginatedDto<EditorDto>> GetAllAsync([Required][FromQuery] GetAllEditorsQuery command)
            => Mediator.Send(command);

        [HttpPut("{editorId:guid}")]
        public Task<EditorDto> UpdateAsync([Required] Guid editorId, [Required][FromBody] UpdateEditorCommand command)
        {
            command.Id = editorId;
            return Mediator.Send(command);
        }
    }
}
