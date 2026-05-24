using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Udpate;

namespace Inkukan.Application.Mappers
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<InkShelf.Domain.Entities.MangaType, TypeDto>()
                .ReverseMap();
            CreateMap<InkShelf.Domain.Entities.MangaType, CreateTypeCommand>()
                .ReverseMap();
            CreateMap<InkShelf.Domain.Entities.MangaType, UpdateTypeCommand>()
                .ReverseMap();
        }
    }
}
