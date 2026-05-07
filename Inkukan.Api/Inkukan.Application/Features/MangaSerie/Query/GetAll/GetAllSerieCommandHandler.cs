using AutoMapper;
using AutoMapper.QueryableExtensions;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.MangaSerie.Query.GetAll
{
    public class GetAllSerieQueryHandler(IMangaSerieRepository mangaSerieRepository, IMapper mapper) 
        : BaseGetAllQueryHandler<Domain.Entities.MangaSerie, MangaSerieDto, GetAllSerieQuery>(mangaSerieRepository, mapper)
    {
    }

    public class GetAllSerieQuery : BaseGetAllQuery<MangaSerieDto>
    {

    }
}
