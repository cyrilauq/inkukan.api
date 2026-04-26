using InkShelf.Api.Extensions;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Commands.Create;
using InkShelf.Application.Features.SerieVolume.Commands.Update;
using InkShelf.Application.Features.SerieVolume.Queries.GetAllBySerie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    [Route("/api/series/{serieId:guid}/volumes")]
    public class SerieVolumeController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync(Guid serieId, [FromForm] CreateSerieVolumeCommand command)
        {
            command.MangaSerieId = serieId;

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<IList<SerieVolumeDto>> GetAllAsync(Guid serieId, [FromQuery] GetAllBySerieQuery query)
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
    }
}
