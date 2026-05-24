using AutoMapper;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Entities;
using Inkukan.Application.Dtos;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Type.Queries.GetAll
{
    public class GetAllCollectionQueryHandler(ICollectionRepository collectionRepository, IMapper mapper)
        : BaseGetAllQueryHandler<MangaCollection, MangaCollectionDto, GetAllCollectionQuery>(collectionRepository, mapper)
    {
    }

    public class GetAllCollectionQuery : BaseGetAllQuery<MangaCollectionDto>
    {

    }
}
