using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaCollection.Commands.Update
{
    public class UpdateMangaCollectionCommandHandler(IBaseRepository<Domain.Entities.MangaCollection> mangaCollectionRepository, IValidator<UpdateMangaCollectionCommand> validator, IMapper mapper)
        : BaseUpdateCommandHandler<UpdateMangaCollectionCommand, MangaCollectionDto, Domain.Entities.MangaCollection>(mangaCollectionRepository, validator, mapper)
    {
        public override Task<bool> AlreadyExistsAsync(UpdateMangaCollectionCommand request)
        {
            return mangaCollectionRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync();
        }

        public override Task BeforeUpdateAsync(UpdateMangaCollectionCommand request, Domain.Entities.MangaCollection enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }

        public override Task<Domain.Entities.MangaCollection?> GetByIdAsync(UpdateMangaCollectionCommand request)
        {
            return mangaCollectionRepository.GetByIdAsync(request.Id);
        }
    }
}
