using InkShelf.Api.Extensions;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Create;
using InkShelf.Application.Features.SerieVolume.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    [Route("/api/series/{serieId:guid}/volumes")]
    public class SerieVolumeController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync(Guid serieId, [FromForm] CreateSerieVolumeCommand command, [FromForm(Name = "vfCover")] IFormFile? vfCover, [FromForm(Name = "voCover")] IFormFile? voCover)
        {
            command.MangaSerieId = serieId;
            command.VOCoverImage = await voCover.ToFileDto();
            command.VFCoverImage = await vfCover.ToFileDto();

            return await Mediator.Send(command);
        }

        [HttpPut("{volumeId:guid}")]
        public async Task<SerieVolumeDto> UpdateAsync(Guid serieId, Guid volumeId, [FromForm] UpdateSerieVolumeCommand command, [FromForm(Name = "vfCover")] IFormFile? vfCover, [FromForm(Name = "voCover")] IFormFile? voCover)
        {
            command.MangaSerieId = serieId;
            command.Id = volumeId;
            command.VOCoverImage = await voCover.ToFileDto();
            command.VFCoverImage = await vfCover.ToFileDto();

            return await Mediator.Send(command);
        }
    }
}
