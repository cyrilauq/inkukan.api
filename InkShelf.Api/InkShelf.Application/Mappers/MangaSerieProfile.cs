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
            CreateMap<MangaSerieDto, MangaSerie>()
                .ReverseMap();

            CreateMap<CreateMangaSerieCommand, MangaSerie>()
                .ReverseMap();
            CreateMap<UpdateMangaSerieCommand, MangaSerie>()
                .ReverseMap();
        }
    }
}
