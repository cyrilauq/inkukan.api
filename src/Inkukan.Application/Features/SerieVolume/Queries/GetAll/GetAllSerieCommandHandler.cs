using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Queries.GetAll
{
    public class GetAllSerieVolumeQueryHandler(ISerieVolumeRepository serieVolumeRepository, IMapper mapper) 
        : BaseGetAllQueryHandler<Domain.Entities.SerieVolume, SerieVolumeDto, GetAllSerieVolumeQuery>(serieVolumeRepository, mapper)
    {
    }

    public class GetAllSerieVolumeQuery : BaseGetAllQuery<SerieVolumeDto>
    {

    }
}
