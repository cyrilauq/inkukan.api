using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Auth.Commands.Register;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers
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
