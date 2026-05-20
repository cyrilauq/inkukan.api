using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Interface;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;

namespace InkShelf.Application.Features.Editor.Commands.Create
{
    public class CreateEditorCommandHandler(IEditorRepository editorRepository, IValidator<CreateEditorCommand> validator, IMapper mapper)
        : IRequestHandler<CreateEditorCommand, EditorDto>, IValidatable<CreateEditorCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateEditorCommand value)
        {
            Domain.Entities.Editor? existingEditor = await editorRepository.GetByNameAsync(value.Name);
            if (existingEditor != null)
                throw new ConflictException("An editor with the same name already exists");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<EditorDto> Handle(CreateEditorCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);
            Domain.Entities.Editor editorToAdd = mapper.Map<Domain.Entities.Editor>(request);
            editorToAdd.ConstitutionDate = DateTime.SpecifyKind(editorToAdd.ConstitutionDate, DateTimeKind.Utc);
            Domain.Entities.Editor addedEditor = await editorRepository.CreateAsync(editorToAdd);
            return mapper.Map<EditorDto>(addedEditor);
        }
    }
}
