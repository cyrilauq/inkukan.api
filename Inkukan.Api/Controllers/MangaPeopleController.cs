using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaPeople.Commands.Create;
using InkShelf.Application.Features.MangaPeople.Commands.Delete;
using InkShelf.Application.Features.MangaPeople.Commands.Update;
using InkShelf.Application.Features.MangaPeople.Queries.GetAll;
using Inkukan.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InkShelf.Api.Controllers
{
    public class MangaPeopleController(IMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaPeopleDto> CreateAsync([FromBody] CreateMangaPeopleCommand command)
            => Mediator.Send(command);

        [HttpDelete("{peopleId:guid}")]
        public Task DeleteAsync(Guid peopleId)
            => Mediator.Send(new DeleteMangaPeopleCommand { Id = peopleId });

        [HttpGet]
        public Task<PaginatedDto<MangaPeopleDto>> GetAllAsync([FromQuery] GetAllMangaPeopleQuery query)
            => Mediator.Send(query);

        [HttpPut("{peppolId:guid}")]
        public Task<MangaPeopleDto> UpdateAsync(Guid peppolId, [FromBody] UpdateMangaPeopleCommand command)
        {
            command.Id = peppolId;
            return Mediator.Send(command);
        }
    }
}
