using AutoMapper;
using InkShelf.Application.Features.Abstractions;
using Inkukan.Application.Dtos;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Type.Queries.GetAll
{
    public class GetAllTypeQueryHandler(ITypeRepository typeRepository, IMapper mapper)
        : BaseGetAllQueryHandler<InkShelf.Domain.Entities.MangaType, TypeDto, GetAllTypeQuery>(typeRepository, mapper)
    {
    }

    public class GetAllTypeQuery : BaseGetAllQuery<TypeDto>
    {

    }
}
