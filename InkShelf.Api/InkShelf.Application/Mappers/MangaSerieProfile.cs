using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaSerie.Command.Create;
using InkShelf.Application.Features.MangaSerie.Command.Update;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
{
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
        }
    }
}
