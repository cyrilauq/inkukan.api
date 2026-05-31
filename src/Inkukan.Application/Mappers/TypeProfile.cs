using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Type.Commands.Create;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<MangaType, TypeDto>()
                .ReverseMap();
            CreateMap<MangaType, CreateTypeCommand>()
                .ReverseMap();
            CreateMap<MangaType, UpdateTypeCommand>()
                .ReverseMap();
        }
    }
}
