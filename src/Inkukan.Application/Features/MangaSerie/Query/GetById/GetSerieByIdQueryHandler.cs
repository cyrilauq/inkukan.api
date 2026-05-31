using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.MangaSerie.Query.GetById
{
    public class GetSerieByIdQuery : BaseGetByIdQuery<MangaSerieDto>
    {

    }
    public class GetSerieByIdQueryHandler(IMangaSerieRepository mangaSerieRepository, IMapper mapper)
        : BaseGetByIdQueryHandler<MangaSerieDto, Domain.Entities.MangaSerie, GetSerieByIdQuery>(mangaSerieRepository, mapper)
    {
    }
}
