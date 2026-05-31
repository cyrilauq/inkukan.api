using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.MangaPeople.Commands.Update
{
    public class UpdateMangaPeopleCommand : IRequest<MangaPeopleDto>
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
    }
}
