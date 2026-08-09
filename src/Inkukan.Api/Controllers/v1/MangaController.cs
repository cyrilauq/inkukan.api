using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Features.MangaSerie.Command.DeleteSerie;
using Inkukan.Application.Features.MangaSerie.Command.Update;
using Inkukan.Application.Features.MangaSerie.Query.GetAll;
using Inkukan.Application.Features.MangaSerie.Query.GetById;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class MangaController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "The created serie")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a new serie")]
    public Task<MangaSerieDto> CreateAsync([Required][FromBody] CreateMangaSerieCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{serieId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Delete a serie")]
    public Task DeleteAsync([Required][FromRoute] Guid serieId, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteSerieCommand() { Id = serieId }, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPut("{mangaId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The updated serie")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Update a serie")]
    public Task<MangaSerieDto> UpdateAsync([Required][FromRoute] Guid mangaId, [Required][FromBody] UpdateMangaSerieCommand command, CancellationToken cancellationToken)
    {
        command.Id = mangaId;
        return Mediator.Send(command, cancellationToken);
    }

    [HttpGet("{mangaId:guid}")]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, "The corresponding serie")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "When the id isn't related to a known serie")]
    [SwaggerOperation(Summary = "Get a serie by id")]
    public Task<MangaSerieDto> GetByIdAsync([Required][FromRoute] Guid mangaId, CancellationToken cancellationToken)
    {
        GetSerieByIdQuery query = new() { Id = mangaId };
        return Mediator.Send(query, cancellationToken);
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get all series", Description = "Get all the serie corresponding to the query inside a paginated result")]
    public Task<PaginatedDto<MangaSerieDto>> GetAllAsync([Required][FromQuery] GetAllSerieQuery query, CancellationToken cancellationToken) 
        => Mediator.Send(query, cancellationToken);
}
