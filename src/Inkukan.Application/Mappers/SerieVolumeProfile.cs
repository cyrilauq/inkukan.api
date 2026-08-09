using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Commands.Create;
using Inkukan.Application.Features.SerieVolume.Commands.Update;
using Inkukan.Application.Services.Implementations;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers;

public class SerieVolumeProfile : Profile
{
    public SerieVolumeProfile()
    {
        CreateMap<SerieVolume, SerieVolumeDto>()
            .ForMember(dest => dest.VFCoverUrls, opt => opt.Ignore())
            .ForMember(dest => dest.VOCoverUrls, opt => opt.Ignore())
            .ForMember(dest => dest.VFCoverPathId, opt => opt.MapFrom(src => src.VFCoverPath))
            .ForMember(dest => dest.VOCoverPathId, opt => opt.MapFrom(src => src.VOCoverPath))
            .AfterMap<SetImageDtoAction>()
            .ReverseMap();
        CreateMap<CreateSerieVolumeCommand, SerieVolume>()
            .ReverseMap();
        CreateMap<UpdateSerieVolumeCommand, SerieVolume>()
            .ReverseMap();
    }
}

public class SetImageDtoAction(VercelBlobOptions vercelBlobOptions) : IMappingAction<SerieVolume, SerieVolumeDto>
{
    public void Process(SerieVolume source, SerieVolumeDto destination, ResolutionContext context)
    {
        destination.VFCoverUrls.LargeUrl = $"{vercelBlobOptions.BlobUrl}/large/{source.VFCoverPath}.webp";
        destination.VFCoverUrls.MediumUrl = $"{vercelBlobOptions.BlobUrl}/medium/{source.VFCoverPath}.webp";
        destination.VFCoverUrls.SmallUrl = $"{vercelBlobOptions.BlobUrl}/small/{source.VFCoverPath}.webp";
        destination.VOCoverUrls.LargeUrl = $"{vercelBlobOptions.BlobUrl}/large/{source.VOCoverPath}.webp";
        destination.VOCoverUrls.MediumUrl = $"{vercelBlobOptions.BlobUrl}/medium/{source.VOCoverPath}.webp";
        destination.VOCoverUrls.SmallUrl = $"{vercelBlobOptions.BlobUrl}/small/{source.VOCoverPath}.webp";
    }
}
