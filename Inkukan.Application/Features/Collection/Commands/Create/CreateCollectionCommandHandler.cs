using AutoMapper;
using FluentValidation;
using InkShelf.Domain.Entities;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.Collection.Commands.Create
{
    public class CreateCollectionCommand : IRequest<CollectionDto>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCollectionCommandHandler(ICollectionRepository collectionRepository, IMapper mapper, IValidator<CreateCollectionCommand> validator)
        : BaseCreateCommandHandler<CreateCollectionCommand, CollectionDto, MangaCollection>(collectionRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(CreateCollectionCommand request)
        {
            return await collectionRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync();
        }

        public override Task BeforeCreateAsync(CreateCollectionCommand request, MangaCollection enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }
    }
}
