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

        [HttpPut("{mangaId:guid}")]
        public Task<MangaSerieDto> UpdateAsync(Guid mangaId, [FromBody] UpdateMangaSerieCommand command)
        {
            command.Id = mangaId;
            return Mediator.Send(command);
        }
    }
}
