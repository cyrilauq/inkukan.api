using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.Type.Commands.Udpate
{
    public class UpdateTypeCommand : IRequest<TypeDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
