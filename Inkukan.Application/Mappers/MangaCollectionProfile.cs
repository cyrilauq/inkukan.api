using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Collection.Commands.Create;

namespace Inkukan.Application.Mappers
{
    public class MangaCollectionProfile : Profile
    {
        public MangaCollectionProfile()
        {
            CreateMap<InkShelf.Domain.Entities.MangaCollection, MangaCollectionDto>()
                .ReverseMap();
            CreateMap<InkShelf.Domain.Entities.MangaCollection, CreateMangaCollectionCommand>()
                .ReverseMap();
        }
    }
}
