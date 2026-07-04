using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Application.Services;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Commands.Update
{
    public class UpdateSerieVolumeCommandHandler(IFileUploader fileUploader, ISerieVolumeRepository volumeRepostory, IHashService hashService, IValidator<UpdateSerieVolumeCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateSerieVolumeCommand, SerieVolumeDto, Domain.Entities.SerieVolume>(volumeRepostory, validator, mapper)
    {
        public override async Task BeforeUpdateAsync(UpdateSerieVolumeCommand request, Domain.Entities.SerieVolume entity, CancellationToken cancellationToken)
        {
            if (request.VFCover != null)
            {
                using MemoryStream stream = new();
                await request.VFCover.CopyToAsync(stream, cancellationToken);
                byte[] vfCoverbytes = stream.ToArray();
                if (await hashService.VerifyHashAsync(entity.VFCoverHash, vfCoverbytes)) return;
                FileDto vfCover = new FileDto(request.VFCover.FileName, vfCoverbytes);
                Guid? vfCoverPath = await fileUploader.UploadAsync(vfCover.Name, vfCover.Content, string.Empty, cancellationToken, SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VFCoverPath = vfCoverPath.ToString();
                entity.VFCoverHash = await hashService.HashBytesAsync(vfCover.Content);
            }
            if (request.VOCover != null)
            {
                using MemoryStream stream = new();
                await request.VOCover.CopyToAsync(stream, cancellationToken);
                byte[] vOCoverbytes = stream.ToArray();
                if (await hashService.VerifyHashAsync(entity.VOCoverHash, vOCoverbytes)) return;
                FileDto voCover = new FileDto(request.VOCover.FileName, vOCoverbytes);
                Guid? voCoverPath = await fileUploader.UploadAsync(voCover.Name, voCover.Content, string.Empty, cancellationToken, SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VOCoverPath = voCoverPath.ToString();
                entity.VOCoverPath = await hashService.HashBytesAsync(voCover.Content);
            }
        }

        public override async Task<Domain.Entities.SerieVolume?> GetByIdAsync(UpdateSerieVolumeCommand request, CancellationToken cancellationToken)
            => await volumeRepostory.GetByIdAsync(request.Id, cancellationToken);
    }
}
