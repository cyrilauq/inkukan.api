using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Editor.Commands.Create;
using InkShelf.Application.Features.Editor.Commands.Update;
using InkShelf.Domain.Entities;

namespace InkShelf.Application.Mappers
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
