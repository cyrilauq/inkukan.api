using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Auth.Commands.Register;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<User, RegisterCommand>()
                .ReverseMap();
        }
    }
}
