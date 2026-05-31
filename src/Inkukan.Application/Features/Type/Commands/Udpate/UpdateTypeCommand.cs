using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.Type.Commands.Udpate
{
    public class UpdateTypeCommand : IRequest<TypeDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
