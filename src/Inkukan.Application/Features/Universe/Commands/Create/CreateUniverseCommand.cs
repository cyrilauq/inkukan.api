using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.Universe.Commands.Create;

public class CreateUniverseCommand : IRequest<UniverseDto>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
