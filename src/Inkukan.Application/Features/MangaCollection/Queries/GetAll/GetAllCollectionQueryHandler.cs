using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaCollection.Queries.GetAll
{
    public class GetAllCollectionQueryHandler(ICollectionRepository collectionRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.MangaCollection, MangaCollectionDto, GetAllCollectionQuery>(collectionRepository, mapper)
    {
    }

    public class GetAllCollectionQuery : BaseGetAllQuery<MangaCollectionDto>
    {

    }
}
