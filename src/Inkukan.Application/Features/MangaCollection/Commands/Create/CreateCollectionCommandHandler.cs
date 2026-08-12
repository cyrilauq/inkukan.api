using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaCollection.Commands.Create
{
    public class CreateMangaCollectionCommand : IRequest<MangaCollectionDto>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCollectionCommandHandler(ICollectionRepository collectionRepository, IMapper mapper, IValidator<CreateMangaCollectionCommand> validator)
        : BaseCreateCommandHandler<CreateMangaCollectionCommand, MangaCollectionDto, Domain.Entities.MangaCollection>(collectionRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(CreateMangaCollectionCommand request, CancellationToken cancellationToken)
        {
            return await collectionRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync(cancellationToken);
        }

        public override Task BeforeCreateAsync(CreateMangaCollectionCommand request, Domain.Entities.MangaCollection enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }
    }
}
