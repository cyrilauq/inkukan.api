using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Commands.Create;
using Inkukan.Application.Features.SerieVolume.Commands.Delete;
using Inkukan.Application.Features.SerieVolume.Commands.Update;
using Inkukan.Application.Features.SerieVolume.Queries.GetAll;
using Inkukan.Application.Features.SerieVolume.Queries.GetAllBySerie;
using Inkukan.Application.Features.SerieVolume.Queries.GetSerieVolumeById;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    [Route("/api/series/{serieId:guid}/volumes")]
    public class SerieVolumeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync([Required] Guid serieId, [Required][FromForm] CreateSerieVolumeCommand command, CancellationToken cancellationToken)
        {
            command.MangaSerieId = serieId;

            return await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{volumeId:guid}")]
        public Task DeleteAsync([Required] Guid volumeId, CancellationToken cancellationToken) 
            => Mediator.Send(new DeleteSerieVolumeCommand() { Id = volumeId }, cancellationToken);

        [HttpGet]
        public async Task<PaginatedDto<SerieVolumeDto>> GetAllAsync([Required] Guid serieId, [Required][FromQuery] GetAllBySerieQuery query, CancellationToken cancellationToken)
        {
            query.SerieId = serieId;
            return await Mediator.Send(query, cancellationToken);
        }

        [HttpPut("{volumeId:guid}")]
        public async Task<SerieVolumeDto> UpdateAsync([Required] Guid serieId, [Required] Guid volumeId, [Required][FromForm] UpdateSerieVolumeCommand command, CancellationToken cancellationToken)
        {
            command.MangaSerieId = serieId;
            command.Id = volumeId;

            return await Mediator.Send(command, cancellationToken);
        }

        [HttpGet("/api/volumes")]
        public Task<PaginatedDto<SerieVolumeDto>> GetAllAsync([Required][FromQuery] GetAllSerieVolumeQuery query, CancellationToken cancellationToken)
        {
            return Mediator.Send(query, cancellationToken);
        }

        [HttpGet("/api/volumes/{volumeId:guid}")]
        public Task<SerieVolumeDto> GetAllAsync([Required] Guid volumeId, CancellationToken cancellationToken)
        {
            return Mediator.Send(new GetSerieVolumeByIdQuery() { Id = volumeId }, cancellationToken);
        }
    }
}
