using AutoMapper;
using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;

namespace Inkukan.Application.Mappers
{
    public class UserListItemMapper : Profile
    {
        public UserListItemMapper()
        {
            CreateMap<UserListItem, UserListItemDto>()
                .ReverseMap();
            CreateMap<UserListItem, AddToUserCollectionCommand>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.SerieVolumeId, opt => opt.MapFrom(src => src.VolumeId))
                .ForMember(dest => dest.ListType, opt => opt.MapFrom(src => src.Type))
                .ReverseMap();
        }
    }
}
