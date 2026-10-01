using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Universe.Commands.Create;
using Inkukan.Application.Features.Universe.Commands.Update;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers;

public class UniverseProfile : Profile
{
    public UniverseProfile()
    {
        CreateMap<Universe, UniverseDto>().ReverseMap();
        CreateMap<CreateUniverseCommand, Universe>();
        CreateMap<UpdateUniverseCommand, Universe>();
    }
}
