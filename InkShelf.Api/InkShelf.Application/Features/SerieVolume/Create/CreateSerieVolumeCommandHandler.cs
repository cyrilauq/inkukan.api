using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Interface;
using InkShelf.Domain.Entities;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;

namespace InkShelf.Application.Features.SerieVolume.Create
{
    public class CreateSerieVolumeCommandHandler(ISerieVolumeRepository serieVolumeRepository, IValidator<CreateSerieVolumeCommand> validator, IMapper mapper)
        : IRequestHandler<CreateSerieVolumeCommand, SerieVolumeDto>, IValidatable<CreateSerieVolumeCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateSerieVolumeCommand value)
        {
            Domain.Entities.SerieVolume existingVolume = await serieVolumeRepository.GetBySerieIdAndVolumeNumber(value.MangaSerieId, value.VolumeNumber);
            if (existingVolume != null)
                throw new ConflictException($"An volume already exist with the number [{value.VolumeNumber}] and for the serie with the id [{value.MangaSerieId}]");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<SerieVolumeDto> Handle(CreateSerieVolumeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.SerieVolume serieToAdd = mapper.Map<Domain.Entities.SerieVolume>(request);
            Domain.Entities.SerieVolume result = await serieVolumeRepository.CreateAsync(serieToAdd);
            return mapper.Map<SerieVolumeDto>(result);
        }
    }
}
