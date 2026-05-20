using AutoMapper;
using FluentValidation;
using InkShelf.Domain.Entities;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.Type.Commands.Create
{
    public class CreateTypeCommand : IRequest<TypeDto>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateTypeCommandHandler(ITypeRepository typeRepository, IMapper mapper, IValidator<CreateTypeCommand> validator)
        : BaseCreateCommandHandler<CreateTypeCommand, TypeDto, InkShelf.Domain.Entities.MangaType>(typeRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(CreateTypeCommand request)
        {
            return await typeRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync();
        }

        public override Task BeforeCreateAsync(CreateTypeCommand request, MangaType enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }
    }
}
