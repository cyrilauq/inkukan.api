using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Application.Features.MangaSerie.Command.Update
{
    public class UpdateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IValidator<UpdateMangaSerieCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateMangaSerieCommand, MangaSerieDto, Domain.Entities.MangaSerie>(mangaSerieRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateMangaSerieCommand request)
        {
            Domain.Entities.MangaSerie? foundMangaSerie = await mangaSerieRepository.GetQuery()
                .Where(ms =>
                    (ms.TitleVF.ToLower().Equals(request.TitleVF.ToLower()) ||
                    ms.TitleVO.ToLower().Equals(request.TitleVO.ToLower())) &&
                    ms.Id != request.Id
                )
                .FirstOrDefaultAsync();
            return foundMangaSerie != null;
        }

        public override async Task<Domain.Entities.MangaSerie?> GetByIdAsync(UpdateMangaSerieCommand request)
        {
            return await mangaSerieRepository.GetByIdAsync(request.Id);
        }
    }
}
