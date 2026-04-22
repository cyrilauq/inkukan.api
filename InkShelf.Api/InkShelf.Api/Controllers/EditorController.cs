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

        [HttpPut]
        public Task<EditorDto> UpdateAsync([FromBody] UpdateEditorCommand command)
            => Mediator.Send(command);
    }
}
