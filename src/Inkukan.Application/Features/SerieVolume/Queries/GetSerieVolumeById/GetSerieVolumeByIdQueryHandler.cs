using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Queries.GetSerieVolumeById
{
    public class GetSerieVolumeByIdQueryHandler(ISerieVolumeRepository serieVolumeRepository, IMapper mapper)
        : BaseGetByIdQueryHandler<SerieVolumeDto, Domain.Entities.SerieVolume, GetSerieVolumeByIdQuery>(serieVolumeRepository, mapper)
    {
    }
}
