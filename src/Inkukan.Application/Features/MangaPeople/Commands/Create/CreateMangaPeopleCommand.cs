using Inkukan.Application.Dtos;
using MediatR;

namespace Inkukan.Application.Features.MangaPeople.Commands.Create
{
    public class CreateMangaPeopleCommand : IRequest<MangaPeopleDto>
    {
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
    }
}
