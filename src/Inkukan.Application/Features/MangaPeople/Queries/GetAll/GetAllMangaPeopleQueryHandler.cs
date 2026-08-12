using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaPeople.Queries.GetAll
{
    public class GetAllMangaPeopleQueryHandler(IMangaPeopleRepository mangaPeopleRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.MangaPeople, MangaPeopleDto, GetAllMangaPeopleQuery>(mangaPeopleRepository, mapper)
    {
    }
}
