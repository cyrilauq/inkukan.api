using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Features.Editor.Commands.Update
{
    public class UpdateEditorCommand : IRequest<EditorDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ConstitutionDate { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? ContactMail { get; set; }
        public string? Description { get; set; }
    }
}
