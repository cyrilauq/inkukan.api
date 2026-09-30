using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Create;

public partial class CreateUniverseCommandHandler(IBaseRepository<UniverseEntity> universeRepository, IValidator<CreateUniverseCommand> validator, IMapper mapper)
    : BaseCreateCommandHandler<CreateUniverseCommand, UniverseDto, UniverseEntity>(universeRepository, validator, mapper)
{
    [GeneratedRegex("[^A-Za-z0-9_]")]
    private static partial Regex NonAlphaNumericRegex();

    public override async Task BeforeCreateAsync(CreateUniverseCommand request, UniverseEntity enttiy, CancellationToken cancellationToken)
    {
        await base.BeforeCreateAsync(request, enttiy, cancellationToken);
        enttiy.Code = NonAlphaNumericRegex().Replace(request.Name.ToLowerInvariant().Replace(' ', '_'), "");
    }

    public override async Task<bool> AlreadyExistsAsync(CreateUniverseCommand request, CancellationToken cancellationToken)
        => await Repository.GetQuery().AnyAsync(universe => universe.Name.ToLower() == request.Name.ToLower(), cancellationToken);
}
