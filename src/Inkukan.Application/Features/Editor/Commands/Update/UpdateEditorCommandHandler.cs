using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Editor.Commands.Update
{
    public class UpdateEditorCommanddHandler(IEditorRepository editorRepository, IValidator<UpdateEditorCommand> validator, IMapper mapper)
        : BaseUpdateCommandHandler<UpdateEditorCommand, EditorDto, Domain.Entities.Editor>(editorRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateEditorCommand request, CancellationToken cancellationToken)
            => await editorRepository.GetByNameAsync(request.Name, cancellationToken) is Domain.Entities.Editor editor && editor.Id != request.Id;

        public override Task<Domain.Entities.Editor?> GetByIdAsync(UpdateEditorCommand request, CancellationToken cancellationToken)
            => editorRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
