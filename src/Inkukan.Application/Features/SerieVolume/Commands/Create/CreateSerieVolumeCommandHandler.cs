using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Application.Services;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Commands.Create
{
    public class CreateSerieVolumeCommandHandler(ISerieVolumeRepository serieVolumeRepository, IFileUploader fileUploader, IHashService hashService, IValidator<CreateSerieVolumeCommand> validator, IMapper mapper)
        : IRequestHandler<CreateSerieVolumeCommand, SerieVolumeDto>, IValidatable<CreateSerieVolumeCommand>
    {
        public async Task<bool> EnsureIsValidAsync(CreateSerieVolumeCommand value, CancellationToken cancellationToken)
        {
            Domain.Entities.SerieVolume? existingVolume = await serieVolumeRepository.GetBySerieIdAndVolumeNumberAsync(value.MangaSerieId, value.VolumeNumber, cancellationToken);
            if (existingVolume != null)
                throw new ConflictException($"An volume already exist with the number [{value.VolumeNumber}] and for the serie with the id [{value.MangaSerieId}]");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value, cancellationToken);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("Some validation errors occured while validating the data", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<SerieVolumeDto> Handle(CreateSerieVolumeCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request, cancellationToken);
            Domain.Entities.SerieVolume serieToAdd = mapper.Map<Domain.Entities.SerieVolume>(request);
            if (request.VFCover != null)
            {
                using MemoryStream stream = new();
                await request.VFCover.CopyToAsync(stream, cancellationToken);
                var vfCoverbytes = stream.ToArray();
                var vfCover = new FileDto(request.VFCover.FileName, vfCoverbytes);
                Guid? vfCoverPath = await fileUploader.UploadAsync(vfCover.Name, vfCover.Content, string.Empty, cancellationToken, SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                serieToAdd.VFCoverPath = vfCoverPath.ToString();
                serieToAdd.VFCoverHash = await hashService.HashBytesAsync(vfCover.Content);
            }
            if (request.VOCover != null)
            {
                using MemoryStream stream = new();
                await request.VOCover.CopyToAsync(stream, cancellationToken);
                var vOCoverbytes = stream.ToArray();
                var voCover = new FileDto(request.VOCover.FileName, vOCoverbytes);
                Guid? voCoverPath = await fileUploader.UploadAsync(voCover.Name, voCover.Content, string.Empty, cancellationToken, SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                serieToAdd.VOCoverPath = voCoverPath.ToString();
                serieToAdd.VOCoverPath = await hashService.HashBytesAsync(voCover.Content);
            }
            if(serieToAdd.VFParutionDate != null)
                serieToAdd.VFParutionDate = DateTime.SpecifyKind(serieToAdd.VFParutionDate.Value, DateTimeKind.Utc);
            serieToAdd.VOParutionDate = DateTime.SpecifyKind(serieToAdd.VOParutionDate, DateTimeKind.Utc);
            
            // TODO : check to add the manga serie inside the dto return

            Domain.Entities.SerieVolume result = await serieVolumeRepository.CreateAsync(serieToAdd, cancellationToken);
            return mapper.Map<SerieVolumeDto>(result);
        }
    }
}
