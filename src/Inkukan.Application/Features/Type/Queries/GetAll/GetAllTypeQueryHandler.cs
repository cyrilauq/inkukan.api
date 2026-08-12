using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Type.Queries.GetAll
{
    public class GetAllTypeQueryHandler(ITypeRepository typeRepository, IMapper mapper)
        : BaseGetAllQueryHandler<MangaType, TypeDto, GetAllTypeQuery>(typeRepository, mapper)
    {
    }

    public class GetAllTypeQuery : BaseGetAllQuery<TypeDto>
    {

    }
}
