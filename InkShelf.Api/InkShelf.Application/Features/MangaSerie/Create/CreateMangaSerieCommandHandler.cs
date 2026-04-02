using AutoMapper;
using InkShelf.Application.Dtos;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Application.Features.MangaSerie.Create
{
    public class CreateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IMapper mapper) : IRequestHandler<CreateMangaSerieCommand, MangaSerieDto>
    {
        public async Task<MangaSerieDto> Handle(CreateMangaSerieCommand command, CancellationToken cancellationToken)
        {
            await EnsureTitlesAreFree(command);
            var addedManga = await mangaSerieRepository.CreateAsync(mapper.Map<Domain.Entities.MangaSerie>(command));
            return mapper.Map<MangaSerieDto>(addedManga);
        }

        private async Task EnsureTitlesAreFree(CreateMangaSerieCommand command)
        {
            Domain.Entities.MangaSerie? foundMangaSerie = await mangaSerieRepository.GetQuery()
                .Where(ms => 
                    ms.TitleVF.ToLower().Contains(command.TitleVF) ||
                    ms.TitleVO.ToLower().Contains(command.TitleVO)
                )
                .FirstOrDefaultAsync();
            if (foundMangaSerie != null)
                throw new ConflictException($"A manga with the title [{command.TitleVO}] or [{command.TitleVF}] already exists");
        }
    }
}
