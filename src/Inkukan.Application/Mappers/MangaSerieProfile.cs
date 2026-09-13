using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Features.MangaSerie.Command.Update;
using Inkukan.Application.Services.Implementations;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers;

public class MangaSerieProfile : Profile
{
    public MangaSerieProfile()
    {
        CreateMap<MangaSerie, MangaSerieDto>()
                        .ForMember(dest => dest.Volumes, opt => opt.ExplicitExpansion());

        CreateMap<MangaSerieDto, MangaSerie>();

        CreateMap<CreateMangaSerieCommand, MangaSerie>()
            .ReverseMap();
        CreateMap<UpdateMangaSerieCommand, MangaSerie>()
            .ReverseMap();

        // TODO : complete
        CreateMap<MangaSerie, SerieListDto>()
            .ForMember(dest => dest.SerieTitleVF, opt => opt.MapFrom(src => src.TitleVF))
            .ForMember(dest => dest.VFCoverPath, opt => opt.MapFrom(src => src.Volumes.FirstOrDefault() == null ? string.Empty : src.Volumes.FirstOrDefault()!.VFCoverPath))
            .ReverseMap();
    }

    public class SetSerieListDtoCoverAction(VercelBlobOptions vercelBlobOptions) : IMappingAction<MangaSerie, SerieListDto>
    {
        public void Process(MangaSerie source, SerieListDto destination, ResolutionContext context)
        {
            if (source.Volumes.FirstOrDefault() is SerieVolume volume)
            {
                destination.VFCoverPath = $"{vercelBlobOptions.BlobUrl}/small/{volume.VFCoverPath}.webp";
            }
        }
    }
}
