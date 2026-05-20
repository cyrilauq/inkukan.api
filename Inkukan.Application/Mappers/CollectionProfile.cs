using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Collection.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Create;

namespace Inkukan.Application.Mappers
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<InkShelf.Domain.Entities.MangaCollection, CollectionDto>()
                .ReverseMap();
            CreateMap<InkShelf.Domain.Entities.MangaCollection, CreateCollectionCommand>()
                .ReverseMap();
        }
    }
}
