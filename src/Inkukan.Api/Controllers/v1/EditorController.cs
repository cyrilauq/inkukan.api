using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Editor.Commands.Create;
using Inkukan.Application.Features.Editor.Commands.Delete;
using Inkukan.Application.Features.Editor.Commands.Update;
using Inkukan.Application.Features.Editor.Queries.GetAll;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Inkukan.Api.Controllers.v1;

public class EditorController(IInkukaMediator mediator) : ApplicationBaseController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK, "The created editor")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Create a new editor")]
    public Task<EditorDto> CreateAsync([Required][FromRoute] CreateEditorCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [HttpDelete("{editorId:guid}")]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Delete an editor")]
    public Task DeleteAsync([Required][FromRoute] Guid editorId, CancellationToken cancellationToken)
        => Mediator.Send(new DeleteEditorCommand { Id = editorId }, cancellationToken);

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "The queried editors")]
    [SwaggerOperation(Summary = "Get editors", Description = "Returns all the editor corresponding to the request in a paginated result")]
    public Task<PaginatedDto<EditorDto>> GetAllAsync([Required][FromQuery] GetAllEditorsQuery command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [HttpPut("{editorId:guid}")]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse(StatusCodes.Status200OK, "The udpated editor")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "If the user is unauthorized")]
    [SwaggerOperation(Summary = "Update an editor")]
    public Task<EditorDto> UpdateAsync([Required][FromRoute] Guid editorId, [Required][FromBody] UpdateEditorCommand command, CancellationToken cancellationToken)
    {
        command.Id = editorId;
        return Mediator.Send(command, cancellationToken);
    }
}
