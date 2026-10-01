using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.Universe.Commands.Update;

public class UpdateUniverseCommand : IRequest<UniverseDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
