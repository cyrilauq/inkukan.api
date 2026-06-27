using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaSerie.Command.Update
{
    public class UpdateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IValidator<UpdateMangaSerieCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateMangaSerieCommand, MangaSerieDto, Domain.Entities.MangaSerie>(mangaSerieRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateMangaSerieCommand request, CancellationToken cancellationToken = default)
        {
            Domain.Entities.MangaSerie? foundMangaSerie = await mangaSerieRepository.GetQuery()
                .Where(ms =>
                    (ms.TitleVF.ToLower().Equals(request.TitleVF.ToLower()) ||
                    ms.TitleVO.ToLower().Equals(request.TitleVO.ToLower())) &&
                    ms.Id != request.Id
                )
                .FirstOrDefaultAsync(cancellationToken);
            return foundMangaSerie != null;
        }

        public override async Task<Domain.Entities.MangaSerie?> GetByIdAsync(UpdateMangaSerieCommand request, CancellationToken cancellationToken = default)
        {
            return await mangaSerieRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
