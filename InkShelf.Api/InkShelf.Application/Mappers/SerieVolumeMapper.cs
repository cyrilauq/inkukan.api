using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Create;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
{
    public class SerieVolumeMapper : Profile
    {
        public SerieVolumeMapper()
        {
            CreateMap<SerieVolume, SerieVolumeDto>()
                .ReverseMap();
            CreateMap<CreateSerieVolumeCommand, SerieVolume>()
                .ReverseMap();
        }
    }
}
