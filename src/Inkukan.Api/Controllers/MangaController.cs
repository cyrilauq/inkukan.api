using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Features.MangaSerie.Command.DeleteSerie;
using Inkukan.Application.Features.MangaSerie.Command.Update;
using Inkukan.Application.Features.MangaSerie.Query.GetAll;
using Inkukan.Application.Features.MangaSerie.Query.GetById;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    public class MangaController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaSerieDto> CreateAsync([Required][FromBody] CreateMangaSerieCommand command)
            => Mediator.Send(command);

        [HttpDelete("{serieId:guid}")]
        public Task DeleteAsync(Guid serieId)
            => Mediator.Send(new DeleteSerieCommand() { Id = serieId });

        [HttpPut("{mangaId:guid}")]
        public Task<MangaSerieDto> UpdateAsync([Required] Guid mangaId, [Required][FromBody] UpdateMangaSerieCommand command)
        {
            command.Id = mangaId;
            return Mediator.Send(command);
        }

        [HttpGet("{mangaId:guid}")]
        public Task<MangaSerieDto> GetByIdAsync([Required] Guid mangaId)
        {
            GetSerieByIdQuery query = new() { Id = mangaId };
            return Mediator.Send(query);
        }

        [HttpGet]
        public Task<PaginatedDto<MangaSerieDto>> GetAllAsync([Required][FromQuery] GetAllSerieQuery query)
        {
            return Mediator.Send(query);
        }
    }
}
