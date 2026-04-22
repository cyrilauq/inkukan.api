using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaPeople.Create;
using InkShelf.Application.Features.MangaPeople.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class MangaPeopleController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaPeopleDto> CreateAsync([FromBody] CreateMangaPeopleCommand command)
            => Mediator.Send(command);

        [HttpPut]
        public Task<MangaPeopleDto> UpdateAsync([FromBody] UpdateMangaPeopleCommand command)
            => Mediator.Send(command);
    }
}
