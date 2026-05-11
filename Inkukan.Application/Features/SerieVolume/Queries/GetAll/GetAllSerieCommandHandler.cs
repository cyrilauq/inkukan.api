using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.SerieVolume.Queries.GetAll
{
    public class GetAllSerieVolumeQueryHandler(ISerieVolumeRepository serieVolumeRepository, IMapper mapper) 
        : BaseGetAllQueryHandler<Domain.Entities.SerieVolume, SerieVolumeDto, GetAllSerieVolumeQuery>(serieVolumeRepository, mapper)
    {
    }

    public class GetAllSerieVolumeQuery : BaseGetAllQuery<SerieVolumeDto>
    {

    }
}
