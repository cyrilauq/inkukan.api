using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Queries.GetAllBySerie;

public class GetAllBySerieQueryHandler(ISerieVolumeRepository serieVolumeRepository, IMapper mapper)
    : BaseGetAllQueryHandler<Domain.Entities.SerieVolume, SerieVolumeDto, GetAllBySerieQuery>(serieVolumeRepository, mapper)
{
    public override IQueryable<SerieVolumeDto> GetQuery(GetAllBySerieQuery query)
    {
        return Repository
            .GetQuery()
            .ProjectTo<SerieVolumeDto>(Mapper.ConfigurationProvider)
            .Where(v => v.MangaSerieId == query.SerieId);
    }
}
