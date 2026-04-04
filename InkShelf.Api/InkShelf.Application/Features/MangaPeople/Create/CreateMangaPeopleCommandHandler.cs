using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using InkShelf.Application.Dtos;
using InkShelf.Application.Extensions;
using InkShelf.Application.Interface;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            await EnsureNotExists(request);
            Domain.Entities.MangaPeople addResult = await mangaPeopleRepository.CreateAsync(mapper.Map<Domain.Entities.MangaPeople>(request));
            return mapper.Map<MangaPeopleDto>(addResult);
        }

        private async Task EnsureNotExists(CreateMangaPeopleCommand command)
        {
            Domain.Entities.MangaPeople? mangaPeople = await mangaPeopleRepository.GetQuery()
                .Where(mp => mp.Firstname.ToLower().Equals(command.Firstname.ToLower()))
                .FirstOrDefaultAsync();
            if (mangaPeople != null)
                throw new ConflictException($"A manga people entity already exists with the lastname [{command.Lastname}] and firstname [{command.Firstname}]");
        }
    }
}
