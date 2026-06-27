using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Editor.Commands.Create
{
    public class CreateEditorCommandHandler(IEditorRepository editorRepository, IValidator<CreateEditorCommand> validator, IMapper mapper)
        : IRequestHandler<CreateEditorCommand, EditorDto>, IValidatable<CreateEditorCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateEditorCommand value, CancellationToken cancellationToken = default)
        {
            Domain.Entities.Editor? existingEditor = await editorRepository.GetByNameAsync(value.Name, cancellationToken);
            if (existingEditor != null)
                throw new ConflictException("An editor with the same name already exists");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value, cancellationToken);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<EditorDto> Handle(CreateEditorCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request, cancellationToken);
            Domain.Entities.Editor editorToAdd = mapper.Map<Domain.Entities.Editor>(request);
            editorToAdd.ConstitutionDate = DateTime.SpecifyKind(editorToAdd.ConstitutionDate, DateTimeKind.Utc);
            Domain.Entities.Editor addedEditor = await editorRepository.CreateAsync(editorToAdd, cancellationToken);
            return mapper.Map<EditorDto>(addedEditor);
        }
    }
}
