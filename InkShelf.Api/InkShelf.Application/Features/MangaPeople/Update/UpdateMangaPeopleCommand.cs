using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.MangaPeople.Update
{
    public class UpdateMangaPeopleCommand : IRequest<MangaPeopleDto>
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
    }
}
