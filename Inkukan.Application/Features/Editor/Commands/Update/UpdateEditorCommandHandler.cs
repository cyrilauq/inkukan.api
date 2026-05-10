using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.Editor.Commands.Update
{
    public class UpdateEditorCommanddHandler(IEditorRepository editorRepository, IValidator<UpdateEditorCommand> validator, IMapper mapper)
        : BaseUpdateCommandHandler<UpdateEditorCommand, EditorDto, Domain.Entities.Editor>(editorRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateEditorCommand request)
            => await editorRepository.GetByNameAsync(request.Name) is Domain.Entities.Editor editor && editor.Id != request.Id;

        public override Task<Domain.Entities.Editor?> GetByIdAsync(UpdateEditorCommand request)
            => editorRepository.GetByIdAsync(request.Id);
    }
}
