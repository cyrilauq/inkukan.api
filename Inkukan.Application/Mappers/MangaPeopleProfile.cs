using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.MangaPeople.Commands.Create;
using Inkukan.Application.Features.MangaPeople.Commands.Update;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers
{
    public class MangaPeopleProfile : Profile
    {
        public MangaPeopleProfile()
        {
            CreateMap<MangaPeople, MangaPeopleDto>()
                .ReverseMap();
            CreateMap<CreateMangaPeopleCommand, MangaPeople>()
                .ReverseMap();
            CreateMap<UpdateMangaPeopleCommand, MangaPeople>()
                .ReverseMap();
        }
    }
}
