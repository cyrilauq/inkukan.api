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
        public Task<MangaSerieDto> CreateAsync([Required][FromBody] CreateMangaSerieCommand command, CancellationToken cancellationToken)
            => Mediator.Send(command, cancellationToken);

        [HttpDelete("{serieId:guid}")]
        public Task DeleteAsync(Guid serieId, CancellationToken cancellationToken)
            => Mediator.Send(new DeleteSerieCommand() { Id = serieId }, cancellationToken);

        [HttpPut("{mangaId:guid}")]
        public Task<MangaSerieDto> UpdateAsync([Required] Guid mangaId, [Required][FromBody] UpdateMangaSerieCommand command, CancellationToken cancellationToken)
        {
            command.Id = mangaId;
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet("{mangaId:guid}")]
        public Task<MangaSerieDto> GetByIdAsync([Required] Guid mangaId, CancellationToken cancellationToken)
        {
            GetSerieByIdQuery query = new() { Id = mangaId };
            return Mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public Task<PaginatedDto<MangaSerieDto>> GetAllAsync([Required][FromQuery] GetAllSerieQuery query, CancellationToken cancellationToken)
        {
            return Mediator.Send(query, cancellationToken);
        }
    }
}
