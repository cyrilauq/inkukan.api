using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaPeople.Commands.Create;
using Inkukan.Application.Features.MangaPeople.Commands.Delete;
using Inkukan.Application.Features.MangaPeople.Commands.Update;
using Inkukan.Application.Features.MangaPeople.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class MangaPeopleController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "The created person")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a new person")]
    public Task<MangaPeopleDto> CreateAsync([Required][FromBody] CreateMangaPeopleCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{peopleId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Delete a person")]
    public Task DeleteAsync([Required][FromRoute] Guid peopleId, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteMangaPeopleCommand { Id = peopleId }, cancellationToken);

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried people")]
    [SwaggerOperation(Summary = "Get all people", Description = "Get all the people corresponding to the query inside a paginated result")]
    public Task<PaginatedDto<MangaPeopleDto>> GetAllAsync([Required][FromQuery] GetAllMangaPeopleQuery query, CancellationToken cancellationToken)
        => Mediator.Send(query, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPut("{peppolId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The udpated person")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Update a person")]
    public Task<MangaPeopleDto> UpdateAsync([Required][FromRoute] Guid peppolId, [Required][FromBody] UpdateMangaPeopleCommand command, CancellationToken cancellationToken)
    {
        command.Id = peppolId;
        return Mediator.Send(command, cancellationToken);
    }
}
