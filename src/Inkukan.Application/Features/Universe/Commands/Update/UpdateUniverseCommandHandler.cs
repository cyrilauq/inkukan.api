using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Features.Universe.Commands.Update;

public partial class UpdateUniverseCommandHandler(IBaseRepository<UniverseEntity> universeRepository, IValidator<UpdateUniverseCommand> validator, IMapper mapper)
    : BaseUpdateCommandHandler<UpdateUniverseCommand, UniverseDto, UniverseEntity>(universeRepository, validator, mapper)
{
    [GeneratedRegex("[^A-Za-z0-9_]")]
    private static partial Regex NonAlphaNumericRegex();

    public override Task<UniverseEntity?> GetByIdAsync(UpdateUniverseCommand request, CancellationToken cancellationToken)
        => Repository.GetByIdAsync(request.Id, cancellationToken);

    public override async Task<bool> AlreadyExistsAsync(UpdateUniverseCommand request, CancellationToken cancellationToken)
    {
        string code = GenerateCode(request.Name ?? string.Empty);
        return await Repository.GetQuery()
            .AnyAsync(universe => (universe.Name.ToLower() == request.Name!.ToLower() || universe.Code == code) && universe.Id != request.Id, cancellationToken);
    }

    public override Task BeforeUpdateAsync(UpdateUniverseCommand request, UniverseEntity entity, CancellationToken cancellationToken)
    {
        entity.Code = GenerateCode(request.Name);
        return Task.CompletedTask;
    }

    private static string GenerateCode(string name)
        => NonAlphaNumericRegex().Replace(name.ToLowerInvariant().Replace(' ', '_'), "");
}
