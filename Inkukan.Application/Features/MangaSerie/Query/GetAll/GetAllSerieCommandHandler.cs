using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaSerie.Query.GetAll
{
    public class GetAllSerieQueryHandler(IMangaSerieRepository mangaSerieRepository, IMapper mapper) 
        : BaseGetAllQueryHandler<Domain.Entities.MangaSerie, MangaSerieDto, GetAllSerieQuery>(mangaSerieRepository, mapper)
    {
    }

    public class GetAllSerieQuery : BaseGetAllQuery<MangaSerieDto>
    {

    }
}
