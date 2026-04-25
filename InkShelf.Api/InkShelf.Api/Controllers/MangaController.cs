using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaSerie.Command.Create;
using InkShelf.Application.Features.MangaSerie.Command.Update;
using InkShelf.Application.Features.MangaSerie.Query.GetAll;
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

        [HttpGet]
        public Task<IList<MangaSerieDto>> GetAllAsync([FromQuery] GetAllSerieQuery query)
        {
            return Mediator.Send(query);
        }
    }
}
