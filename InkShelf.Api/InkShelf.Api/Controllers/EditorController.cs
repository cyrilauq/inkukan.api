using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Editor.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class EditorController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<EditorDto> CreateAsync(CreateEditorCommand command)
            => Mediator.Send(command);
    }
}
