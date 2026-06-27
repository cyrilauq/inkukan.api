using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaSerie.Command.Create
{
    public class CreateMangaSerieCommandHandler(IMangaSerieRepository mangaSerieRepository, IValidator<CreateMangaSerieCommand> validator, IMapper mapper) 
        : IRequestHandler<CreateMangaSerieCommand, MangaSerieDto>, IValidatable<CreateMangaSerieCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateMangaSerieCommand value, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(value, cancellationToken);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<MangaSerieDto> Handle(CreateMangaSerieCommand command, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(command);
            await EnsureTitlesAreFree(command);
            Domain.Entities.MangaSerie addedManga = await mangaSerieRepository.CreateAsync(mapper.Map<Domain.Entities.MangaSerie>(command), cancellationToken);
            return mapper.Map<MangaSerieDto>(addedManga);
        }

        private async Task EnsureTitlesAreFree(CreateMangaSerieCommand command, CancellationToken cancellationToken = default)
        {
            Domain.Entities.MangaSerie? foundMangaSerie = await mangaSerieRepository.GetQuery()
                .Where(ms => 
                    ms.TitleVF.ToLower().Equals(command.TitleVF.ToLower()) ||
                    ms.TitleVO.ToLower().Equals(command.TitleVO.ToLower())
                )
                .FirstOrDefaultAsync(cancellationToken);
            if (foundMangaSerie != null)
                throw new ConflictException($"A manga with the title [{command.TitleVO}] or [{command.TitleVF}] already exists");
        }
    }
}
