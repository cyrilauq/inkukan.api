using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Editor.Commands.Create;
using Inkukan.Application.Features.Editor.Commands.Update;
using Inkukan.Domain.Entities;

namespace Inkukan.Application.Mappers
{
    public class EditorProfile : Profile
    {
        public EditorProfile()
        {
            CreateMap<EditorDto, Editor>()
                .ReverseMap();
            CreateMap<CreateEditorCommand, Editor>()
                .ReverseMap();
            CreateMap<UpdateEditorCommand, Editor>()
                .ReverseMap();
        }
    }
}
