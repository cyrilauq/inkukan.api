using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Universe.Queries.GetAll;

public class GetAllUniversesQueryHandler(IBaseRepository<Domain.Entities.Universe> universeRepository, IMapper mapper)
    : BaseGetAllQueryHandler<Domain.Entities.Universe, UniverseDto, GetAllUniversesQuery>(universeRepository, mapper)
{
}
