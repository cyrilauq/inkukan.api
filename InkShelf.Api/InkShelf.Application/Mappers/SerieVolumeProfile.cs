using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.SerieVolume.Create;
using InkShelf.Application.Services.Implementations;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
{
    public class SerieVolumeProfile : Profile
    {
        public SerieVolumeProfile()
        {
            CreateMap<SerieVolume, SerieVolumeDto>()
                .ForMember(dest => dest.VFCoverPath, opt => opt.Ignore())
                .ForMember(dest => dest.VOCoverPath, opt => opt.Ignore())
                .AfterMap<SetImageDtoAction>()
                .ReverseMap();
            CreateMap<CreateSerieVolumeCommand, SerieVolume>()
                .ReverseMap();
        }
    }

    public class SetImageDtoAction(VercelBlobOptions vercelBlobOptions) : IMappingAction<SerieVolume, SerieVolumeDto>
    {
        public void Process(SerieVolume source, SerieVolumeDto destination, ResolutionContext context)
        {
            destination.VFCoverPath.LargeUrl = $"{vercelBlobOptions.BlobUrl}/large/{source.VFCoverPath}.webp";
            destination.VFCoverPath.MediumUrl = $"{vercelBlobOptions.BlobUrl}/medium/{source.VFCoverPath}.webp";
            destination.VFCoverPath.SmallUrl = $"{vercelBlobOptions.BlobUrl}/small/{source.VFCoverPath}.webp";
            destination.VOCoverPath.LargeUrl = $"{vercelBlobOptions.BlobUrl}/large/{source.VOCoverPath}.webp";
            destination.VOCoverPath.MediumUrl = $"{vercelBlobOptions.BlobUrl}/medium/{source.VOCoverPath}.webp";
            destination.VOCoverPath.SmallUrl = $"{vercelBlobOptions.BlobUrl}/small/{source.VOCoverPath}.webp";
        }
    }
}
