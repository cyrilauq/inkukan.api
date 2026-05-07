using AutoMapper;
using AutoMapper.QueryableExtensions;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.SerieVolume.Queries.GetAllBySerie
{
    public class GetAllBySerieQueryHandler(ISerieVolumeRepository serieVolumeRepository, IMapper mapper)
        : BaseGetAllQueryHandler<Domain.Entities.SerieVolume, SerieVolumeDto, GetAllBySerieQuery>(serieVolumeRepository, mapper)
    {
        public override IQueryable<SerieVolumeDto> GetQuery(GetAllBySerieQuery query)
        {
            return serieVolumeRepository
                .GetQuery()
                .ProjectTo<SerieVolumeDto>(mapper.ConfigurationProvider)
                .Where(v => v.MangaSerieId == query.SerieId);
        }
    }
}
