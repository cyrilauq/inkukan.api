using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InkShelf.Application.Dtos;
using InkShelf.Application.Extensions;
using InkShelf.Application.Interface;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;

namespace InkShelf.Application.Features.MangaPeople.Create
{
    public class CreateMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository, IValidator<CreateMangaPeopleCommand> validator, IMapper mapper) 
        : IRequestHandler<CreateMangaPeopleCommand, MangaPeopleDto>, IValidatable<CreateMangaPeopleCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateMangaPeopleCommand value)
        {
            ValidationResult validationResult = await validator.ValidateAsync(value);
            return validationResult.IsValid ? true 
                : throw new EntityValidationException("A validation error occured while adding the manga people object", validationResult.GetErrorMessages());
        }

        public async Task<MangaPeopleDto> Handle(CreateMangaPeopleCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);
            Domain.Entities.MangaPeople addResult = await mangaPeopleRepository.CreateAsync(mapper.Map<Domain.Entities.MangaPeople>(request));
            return mapper.Map<MangaPeopleDto>(addResult);
        }
    }
}
