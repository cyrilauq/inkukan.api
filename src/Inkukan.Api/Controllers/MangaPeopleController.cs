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
        public Task<MangaPeopleDto> CreateAsync([Required][FromBody] CreateMangaPeopleCommand command, CancellationToken cancellationToken)
            => Mediator.Send(command, cancellationToken);

        [HttpDelete("{peopleId:guid}")]
        public Task DeleteAsync([Required] Guid peopleId, CancellationToken cancellationToken)
            => Mediator.Send(new DeleteMangaPeopleCommand { Id = peopleId }, cancellationToken);

        [HttpGet]
        public Task<PaginatedDto<MangaPeopleDto>> GetAllAsync([Required][FromQuery] GetAllMangaPeopleQuery query, CancellationToken cancellationToken)
            => Mediator.Send(query, cancellationToken);

        [HttpPut("{peppolId:guid}")]
        public Task<MangaPeopleDto> UpdateAsync([Required] Guid peppolId, [Required][FromBody] UpdateMangaPeopleCommand command, CancellationToken cancellationToken)
        {
            command.Id = peppolId;
            return Mediator.Send(command, cancellationToken);
        }
    }
}
