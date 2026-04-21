using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Application.Services;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.SerieVolume.Update
{
    public class UpdateSerieVolumeCommandHandler(IFileUploader fileUploader, ISerieVolumeRepository volumeRepostory, IHashService hashService, IValidator<UpdateSerieVolumeCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateSerieVolumeCommand, SerieVolumeDto, Domain.Entities.SerieVolume>(volumeRepostory, validator, mapper)
    {
        public override async Task AfterUpdateAsync(UpdateSerieVolumeCommand request, Domain.Entities.SerieVolume entity, CancellationToken cancellationToken)
        {
            if (request.VFCoverImage is FileDto vfCover && !await hashService.VerifyHashAsync(entity.VFCoverHash, vfCover.Content))
            {
                Guid? vfCoverPath = await fileUploader.UploadAsync(vfCover.Name, vfCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VFCoverPath = vfCoverPath.ToString();
                entity.VFCoverHash = await hashService.HashBytesAsync(vfCover.Content);
            }
            if (request.VOCoverImage is FileDto voCover && !await hashService.VerifyHashAsync(entity.VOCoverHash, voCover.Content))
            {
                Guid? voCoverPath = await fileUploader.UploadAsync(voCover.Name, voCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VOCoverPath = voCoverPath.ToString();
                entity.VOCoverPath = await hashService.HashBytesAsync(voCover.Content);
            }
        }

        public override async Task<Domain.Entities.SerieVolume?> GetByIdAsync(UpdateSerieVolumeCommand request)
            => await volumeRepostory.GetByIdAsync(request.Id);
    }
}
