using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Commands.Create;
using Inkukan.Application.Features.SerieVolume.Commands.Delete;
using Inkukan.Application.Features.SerieVolume.Commands.Update;
using Inkukan.Application.Features.SerieVolume.Queries.GetAll;
using Inkukan.Application.Features.SerieVolume.Queries.GetAllBySerie;
using Inkukan.Application.Features.SerieVolume.Queries.GetSerieVolumeById;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

[Route("/v{version:apiVersion}/series/{serieId:guid}/volumes")]
public class SerieVolumeController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "The created volume")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a volume")]
    public async Task<SerieVolumeDto> CreateAsync([Required][FromRoute] Guid serieId, [Required][FromForm] CreateSerieVolumeCommand command, CancellationToken cancellationToken)
    {
        command.MangaSerieId = serieId;

        return await Mediator.Send(command, cancellationToken);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{volumeId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Delete a volume")]
    public Task DeleteAsync([Required] Guid volumeId, CancellationToken cancellationToken) 
        => Mediator.Send(new DeleteSerieVolumeCommand() { Id = volumeId }, cancellationToken);

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "The volume of the queried serie")]
    [SwaggerOperation(Summary = "Get serie's volumes", Description = "Get all the volumes from a serie")]
    public async Task<PaginatedDto<SerieVolumeDto>> GetAllAsync([Required][FromRoute] Guid serieId, [Required][FromQuery] GetAllBySerieQuery query, CancellationToken cancellationToken)
    {
        query.SerieId = serieId;
        return await Mediator.Send(query, cancellationToken);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{volumeId:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The updated volume", typeof(SerieVolumeDto))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Update a volume")]
    public async Task<SerieVolumeDto> UpdateAsync([Required][FromRoute] Guid serieId, [Required][FromRoute] Guid volumeId, [Required][FromForm] UpdateSerieVolumeCommand command, CancellationToken cancellationToken)
    {
        command.MangaSerieId = serieId;
        command.Id = volumeId;

        return await Mediator.Send(command, cancellationToken);
    }

    [HttpGet("/v{version:apiVersion}/volumes")]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried volumes", typeof(PaginatedDto<SerieVolumeDto>))]
    [SwaggerOperation(Summary = "Get all volumes", Description = "Get all the volumes corresponding to the query inside a paginated result")]
    public Task<PaginatedDto<SerieVolumeDto>> GetAllAsync([Required][FromQuery] GetAllSerieVolumeQuery query, CancellationToken cancellationToken) 
        => Mediator.Send(query, cancellationToken);

    [HttpGet("/v{version:apiVersion}/volumes/{volumeId:guid}")]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried volume")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "When the id isn't related to a known volume")]
    [SwaggerOperation(Summary = "Get volume by id", Description = "Get the volume corresponding to the given id")]
    public Task<SerieVolumeDto> GetAllAsync([Required][FromRoute] Guid volumeId, CancellationToken cancellationToken) 
        => Mediator.Send(new GetSerieVolumeByIdQuery() { Id = volumeId }, cancellationToken);
}
