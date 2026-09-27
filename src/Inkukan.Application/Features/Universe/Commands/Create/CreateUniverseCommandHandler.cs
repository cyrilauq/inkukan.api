using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Create;

public class CreateUniverseCommandHandler(IBaseRepository<UniverseEntity> universeRepository, IValidator<CreateUniverseCommand> validator, IMapper mapper)
    : BaseCreateCommandHandler<CreateUniverseCommand, UniverseDto, UniverseEntity>(universeRepository, validator, mapper)
{
    public override async Task<bool> AlreadyExistsAsync(CreateUniverseCommand request, CancellationToken cancellationToken)
        => await Repository.GetQuery().AnyAsync(universe => universe.Code.ToLower() == request.Code.ToLower(), cancellationToken);
}
