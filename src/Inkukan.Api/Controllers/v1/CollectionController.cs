using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaCollection.Commands.Create;
using Inkukan.Application.Features.MangaCollection.Commands.Delete;
using Inkukan.Application.Features.MangaCollection.Commands.Update;
using Inkukan.Application.Features.MangaCollection.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class CollectionController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, "All the serie's collection", typeof(UserDto))]
    [SwaggerOperation(Summary = "Get serie's collection", Description = "Return all the serie related to that collection")]
    public Task<PaginatedDto<MangaCollectionDto>> GetAllAsync([Required][FromQuery] GetAllCollectionQuery query, CancellationToken cancellationToken)
        => Mediator.Send(query, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "The added serie's collection item", typeof(UserDto))]
    [SwaggerOperation(Summary = "Add aserie's collection", Description = "Add a serie's collection")]
    public Task<MangaCollectionDto> CreateAsync([Required][FromBody] CreateMangaCollectionCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The update serie's collection", typeof(UserDto))]
    [SwaggerOperation(Summary = "Update a serie's collection")]
    public Task<MangaCollectionDto> UpdateAsync([Required][FromRoute] Guid id, [Required][FromBody] UpdateMangaCollectionCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        return Mediator.Send(command, cancellationToken);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(UserDto))]
    [SwaggerOperation(Summary = "Delete a serie's collection")]
    public Task DeleteAsync([Required][FromRoute] Guid id, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteMangaCollectionCommand() { Id = id }, cancellationToken);
}
