using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Features.MangaSerie.Command.DeleteSerie;
using Inkukan.Application.Features.MangaSerie.Command.Update;
using Inkukan.Application.Features.MangaSerie.Query.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    public class MangaController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaSerieDto> CreateAsync([FromBody] CreateMangaSerieCommand command)
            => Mediator.Send(command);

        [HttpDelete("{serieId:guid}")]
        public Task DeleteAsync(Guid serieId)
            => Mediator.Send(new DeleteSerieCommand() { Id = serieId });

        [HttpPut("{mangaId:guid}")]
        public Task<MangaSerieDto> UpdateAsync(Guid mangaId, [FromBody] UpdateMangaSerieCommand command)
        {
            command.Id = mangaId;
            return Mediator.Send(command);
        }

        [HttpGet]
        public Task<PaginatedDto<MangaSerieDto>> GetAllAsync([FromQuery] GetAllSerieQuery query)
        {
            return Mediator.Send(query);
        }
    }
}
