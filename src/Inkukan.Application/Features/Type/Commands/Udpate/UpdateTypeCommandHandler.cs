using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Extensions;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.Type.Commands.Udpate
{
    public class UpdateTypeCommandHandler(IBaseRepository<MangaType> typeRepository, IValidator<UpdateTypeCommand> validator, IMapper mapper)
        : BaseUpdateCommandHandler<UpdateTypeCommand, TypeDto, MangaType>(typeRepository, validator, mapper)
    {
        public override Task<bool> AlreadyExistsAsync(UpdateTypeCommand request)
        {
            return typeRepository.GetQuery()
                .Where(t => t.Name.ToLower() == request.Name.ToLower())
                .AnyAsync();
        }

        public override Task BeforeUpdateAsync(UpdateTypeCommand request, MangaType enttiy, CancellationToken cancellationToken)
        {
            enttiy.Code = request.Name
                .ToLower()
                .Replace(" ", "_")
                .RemoveNonAsciiCharacters();

            return Task.CompletedTask;
        }

        public override Task<MangaType?> GetByIdAsync(UpdateTypeCommand request)
        {
            return typeRepository.GetByIdAsync(request.Id);
        }
    }
}
