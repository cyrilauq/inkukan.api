using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaPeople.Commands.Create;
using Inkukan.Application.Features.MangaPeople.Commands.Delete;
using Inkukan.Application.Features.MangaPeople.Commands.Update;
using Inkukan.Application.Features.MangaPeople.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers
{
    public class MangaPeopleController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
    {
        [HttpPost]
        public Task<MangaPeopleDto> CreateAsync([Required][FromBody] CreateMangaPeopleCommand command)
            => Mediator.Send(command);

        [HttpDelete("{peopleId:guid}")]
        public Task DeleteAsync([Required] Guid peopleId)
            => Mediator.Send(new DeleteMangaPeopleCommand { Id = peopleId });

        [HttpGet]
        public Task<PaginatedDto<MangaPeopleDto>> GetAllAsync([Required][FromQuery] GetAllMangaPeopleQuery query)
            => Mediator.Send(query);

        [HttpPut("{peppolId:guid}")]
        public Task<MangaPeopleDto> UpdateAsync([Required] Guid peppolId, [Required][FromBody] UpdateMangaPeopleCommand command)
        {
            command.Id = peppolId;
            return Mediator.Send(command);
        }
    }
}
