using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.MangaSerie.Command.Update
{
    public class UpdateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IValidator<UpdateMangaSerieCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateMangaSerieCommand, MangaSerieDto, Domain.Entities.MangaSerie>(mangaSerieRepository, validator, mapper)
    {
    }
}
