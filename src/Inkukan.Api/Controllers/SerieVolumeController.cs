using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Commands.Create;
using Inkukan.Application.Features.SerieVolume.Commands.Delete;
using Inkukan.Application.Features.SerieVolume.Commands.Update;
using Inkukan.Application.Features.SerieVolume.Queries.GetAll;
using Inkukan.Application.Features.SerieVolume.Queries.GetAllBySerie;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    [Route("/api/series/{serieId:guid}/volumes")]
    public class SerieVolumeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync(Guid serieId, [FromForm] CreateSerieVolumeCommand command)
        {
            command.MangaSerieId = serieId;

            return await Mediator.Send(command);
        }

        [HttpDelete("{volumeId:guid}")]
        public Task DeleteAsync(Guid volumeId) 
            => Mediator.Send(new DeleteSerieVolumeCommand() { Id = volumeId });

        [HttpGet]
        public async Task<PaginatedDto<SerieVolumeDto>> GetAllAsync(Guid serieId, [FromQuery] GetAllBySerieQuery query)
        {
            query.SerieId = serieId;
            return await Mediator.Send(query);
        }

        [HttpPut("{volumeId:guid}")]
        public async Task<SerieVolumeDto> UpdateAsync(Guid serieId, Guid volumeId, [FromForm] UpdateSerieVolumeCommand command)
        {
            command.MangaSerieId = serieId;
            command.Id = volumeId;

            return await Mediator.Send(command);
        }

        [HttpGet("/api/volumes")]
        public Task<PaginatedDto<SerieVolumeDto>> GetAllAsync([FromQuery] GetAllSerieVolumeQuery query)
        {
            return Mediator.Send(query);
        }
    }
}
