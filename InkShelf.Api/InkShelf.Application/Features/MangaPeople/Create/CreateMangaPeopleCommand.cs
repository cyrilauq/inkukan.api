using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.MangaPeople.Create
{
    public class CreateMangaPeopleCommand : IRequest<MangaPeopleDto>
    {
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
    }
}
