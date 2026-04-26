using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Interface;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Application.Features.MangaSerie.Command.Create
{
    public class CreateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IValidator<CreateMangaSerieCommand> validator, IMapper mapper) 
        : IRequestHandler<CreateMangaSerieCommand, MangaSerieDto>, IValidatable<CreateMangaSerieCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateMangaSerieCommand value)
        {
            var validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<MangaSerieDto> Handle(CreateMangaSerieCommand command, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(command);
            await EnsureTitlesAreFree(command);
            Domain.Entities.MangaSerie addedManga = await mangaSerieRepository.CreateAsync(mapper.Map<Domain.Entities.MangaSerie>(command));
            return mapper.Map<MangaSerieDto>(addedManga);
        }

        private async Task EnsureTitlesAreFree(CreateMangaSerieCommand command)
        {
            Domain.Entities.MangaSerie? foundMangaSerie = await mangaSerieRepository.GetQuery()
                .Where(ms => 
                    ms.TitleVF.ToLower().Equals(command.TitleVF.ToLower()) ||
                    ms.TitleVO.ToLower().Equals(command.TitleVO.ToLower())
                )
                .FirstOrDefaultAsync();
            if (foundMangaSerie != null)
                throw new ConflictException($"A manga with the title [{command.TitleVO}] or [{command.TitleVF}] already exists");
        }
    }
}
