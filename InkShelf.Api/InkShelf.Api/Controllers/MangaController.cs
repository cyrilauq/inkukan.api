using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaSerie.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class MangaController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaSerieDto> CreateAsync([FromBody] CreateMangaSerieCommand command)
            => Mediator.Send(command);
    }
}
