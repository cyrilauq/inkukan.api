using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.MangaPeople.Commands.Create;
using InkShelf.Application.Features.MangaPeople.Commands.Update;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
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
