using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.MangaPeople.Queries.GetAll
{
    public class GetAllMangaPeopleQueryHandler(IMangaPeopleRepository mangaPeopleRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.MangaPeople, MangaPeopleDto, GetAllMangaPeopleQuery>(mangaPeopleRepository, mapper)
    {
    }
}
