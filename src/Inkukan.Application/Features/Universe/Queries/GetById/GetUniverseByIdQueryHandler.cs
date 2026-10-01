using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Universe.Queries.GetById;

public class GetUniverseByIdQueryHandler(IBaseRepository<Domain.Entities.Universe> universeRepository, IMapper mapper)
    : BaseGetByIdQueryHandler<UniverseDto, Domain.Entities.Universe, GetUniverseByIdQuery>(universeRepository, mapper)
{
}
