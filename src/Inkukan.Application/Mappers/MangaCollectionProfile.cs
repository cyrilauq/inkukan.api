using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaCollection.Commands.Create;
using Inkukan.Application.Features.MangaCollection.Commands.Update;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers
{
    public class MangaCollectionProfile : Profile
    {
        public MangaCollectionProfile()
        {
            CreateMap<MangaCollection, MangaCollectionDto>()
                .ReverseMap();
            CreateMap<MangaCollection, CreateMangaCollectionCommand>()
                .ReverseMap();
            CreateMap<MangaCollection, UpdateMangaCollectionCommand>()
                .ReverseMap();
        }
    }
}
