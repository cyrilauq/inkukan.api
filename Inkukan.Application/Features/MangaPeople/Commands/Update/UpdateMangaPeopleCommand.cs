using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.MangaPeople.Commands.Update
{
    public class UpdateMangaPeopleCommand : IRequest<MangaPeopleDto>
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
    }
}
