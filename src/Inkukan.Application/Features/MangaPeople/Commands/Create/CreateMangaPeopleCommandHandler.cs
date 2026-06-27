using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaPeople.Commands.Create
{
    public class CreateMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository, IValidator<CreateMangaPeopleCommand> validator, IMapper mapper) 
        : IRequestHandler<CreateMangaPeopleCommand, MangaPeopleDto>, IValidatable<CreateMangaPeopleCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateMangaPeopleCommand value, CancellationToken cancellationToken = default)
        {
            ValidationResult validationResult = await validator.ValidateAsync(value, cancellationToken);
            return validationResult.IsValid ? true 
                : throw new EntityValidationException("A validation error occured while adding the manga people object", validationResult.GetErrorMessages());
        }

        public async Task<MangaPeopleDto> Handle(CreateMangaPeopleCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request, cancellationToken);
            await EnsureNotExists(request, cancellationToken);
            Domain.Entities.MangaPeople addResult = await mangaPeopleRepository.CreateAsync(mapper.Map<Domain.Entities.MangaPeople>(request), cancellationToken);
            return mapper.Map<MangaPeopleDto>(addResult);
        }

        private async Task EnsureNotExists(CreateMangaPeopleCommand command, CancellationToken cancellationToken = default)
        {
            Domain.Entities.MangaPeople? mangaPeople = await mangaPeopleRepository.GetQuery()
                .Where(mp => 
                    mp.Firstname.ToLower().Equals(command.Firstname.ToLower()) &&
                    mp.Lastname.ToLower().Equals(command.Lastname.ToLower())
                )
                .FirstOrDefaultAsync(cancellationToken);
            if (mangaPeople != null)
                throw new ConflictException($"A manga people entity already exists with the lastname [{command.Lastname}] and firstname [{command.Firstname}]");
        }
    }
}
