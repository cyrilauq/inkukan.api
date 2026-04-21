using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaSerie.Create;
using InkShelf.Application.Features.MangaSerie.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class MangaController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaSerieDto> CreateAsync([FromBody] CreateMangaSerieCommand command)
            => Mediator.Send(command);

        [HttpPut]
        public Task<MangaSerieDto> UpdateAsync([FromBody] UpdateMangaSerieCommand command)
            => Mediator.Send(command);
    }
}
