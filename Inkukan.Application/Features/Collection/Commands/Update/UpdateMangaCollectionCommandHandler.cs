using AutoMapper;
using FluentValidation;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Entities;
using InkShelf.Domain.Repositories;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Type.Commands.Udpate;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.Collection.Commands.Update
{
    public class UpdateMangaCollectionCommandHandler(IBaseRepository<MangaCollection> mangaCollectionRepository, IValidator<UpdateMangaCollectionCommand> validator, IMapper mapper)
        : BaseUpdateCommandHandler<UpdateMangaCollectionCommand, MangaCollectionDto, MangaCollection>(mangaCollectionRepository, validator, mapper)
    {
        public override Task<bool> AlreadyExistsAsync(UpdateMangaCollectionCommand request)
        {
            return mangaCollectionRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync();
        }

        public override Task BeforeUpdateAsync(UpdateMangaCollectionCommand request, MangaCollection enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }

        public override Task<MangaCollection?> GetByIdAsync(UpdateMangaCollectionCommand request)
        {
            return mangaCollectionRepository.GetByIdAsync(request.Id);
        }
    }
}
