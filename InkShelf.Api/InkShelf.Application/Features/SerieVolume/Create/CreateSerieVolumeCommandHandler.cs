using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Interface;
using InkShelf.Application.Services;
using InkShelf.Domain.Entities;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;

namespace InkShelf.Application.Features.SerieVolume.Create
{
    public class CreateSerieVolumeCommandHandler(ISerieVolumeRepository serieVolumeRepository, IFileUploader fileUploader, IHashService hashService, IValidator<CreateSerieVolumeCommand> validator, IMapper mapper)
        : IRequestHandler<CreateSerieVolumeCommand, SerieVolumeDto>, IValidatable<CreateSerieVolumeCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateSerieVolumeCommand value)
        {
            Domain.Entities.SerieVolume? existingVolume = await serieVolumeRepository.GetBySerieIdAndVolumeNumber(value.MangaSerieId, value.VolumeNumber);
            if (existingVolume != null)
                throw new ConflictException($"An volume already exist with the number [{value.VolumeNumber}] and for the serie with the id [{value.MangaSerieId}]");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("Some validation errors occured while validating the data", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<SerieVolumeDto> Handle(CreateSerieVolumeCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);
            Domain.Entities.SerieVolume serieToAdd = mapper.Map<Domain.Entities.SerieVolume>(request);
            if (request.VFCoverImage is FileDto vfCover)
            {
                Guid? vfCoverPath = await fileUploader.UploadAsync(vfCover.Name, vfCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                serieToAdd.VFCoverPath = vfCoverPath.ToString();
                serieToAdd.VFCoverHash = await hashService.HashBytesAsync(vfCover.Content);
            }
            if (request.VOCoverImage is FileDto voCover)
            {
                Guid? voCoverPath = await fileUploader.UploadAsync(voCover.Name, voCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                serieToAdd.VOCoverPath = voCoverPath.ToString();
                serieToAdd.VOCoverPath = await hashService.HashBytesAsync(voCover.Content);
            }
            Domain.Entities.SerieVolume result = await serieVolumeRepository.CreateAsync(serieToAdd, cancellationToken);
            return mapper.Map<SerieVolumeDto>(result);
        }
    }
}
