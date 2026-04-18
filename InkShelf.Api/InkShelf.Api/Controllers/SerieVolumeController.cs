using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class SerieVolumeController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public async Task<SerieVolumeDto> CreateAsync([FromBody] CreateSerieVolumeCommand command)
            => await Mediator.Send(command);
    }
}
