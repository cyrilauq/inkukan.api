using InkShelf.Api.Extensions;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Create;
using InkShelf.Application.Features.SerieVolume.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class SerieVolumeController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync([FromForm] CreateSerieVolumeCommand command, [FromForm(Name = "vfCover")] IFormFile? vfCover, [FromForm(Name = "voCover")] IFormFile? voCover)
        {
            command.VOCoverImage = await voCover.ToFileDto();
            command.VFCoverImage = await vfCover.ToFileDto();

            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<SerieVolumeDto> UpdateAsync([FromForm] UpdateSerieVolumeCommand command, [FromForm(Name = "vfCover")] IFormFile? vfCover, [FromForm(Name = "voCover")] IFormFile? voCover)
        {
            command.VOCoverImage = await voCover.ToFileDto();
            command.VFCoverImage = await vfCover.ToFileDto();

            return await Mediator.Send(command);
        }
    }
}
