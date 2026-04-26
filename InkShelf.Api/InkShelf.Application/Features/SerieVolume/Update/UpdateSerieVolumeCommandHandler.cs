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
        public override async Task BeforeUpdateAsync(UpdateSerieVolumeCommand request, Domain.Entities.SerieVolume entity, CancellationToken cancellationToken)
        {
            if (request.VFCover != null)
            {
                using MemoryStream stream = new();
                await request.VFCover.CopyToAsync(stream, cancellationToken);
                var vfCoverbytes = stream.ToArray();
                if (await hashService.VerifyHashAsync(entity.VFCoverHash, vfCoverbytes)) return;
                var vfCover = new FileDto(request.VFCover.FileName, vfCoverbytes);
                Guid? vfCoverPath = await fileUploader.UploadAsync(vfCover.Name, vfCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VFCoverPath = vfCoverPath.ToString();
                entity.VFCoverHash = await hashService.HashBytesAsync(vfCover.Content);
            }
            if (request.VOCover != null)
            {
                using MemoryStream stream = new();
                await request.VOCover.CopyToAsync(stream, cancellationToken);
                var vOCoverbytes = stream.ToArray();
                if (await hashService.VerifyHashAsync(entity.VOCoverHash, vOCoverbytes)) return;
                var voCover = new FileDto(request.VOCover.FileName, vOCoverbytes);
                Guid? voCoverPath = await fileUploader.UploadAsync(voCover.Name, voCover.Content, "", SupportedFileType.PNG, SupportedFileType.JPG, SupportedFileType.JPEG);
                entity.VOCoverPath = voCoverPath.ToString();
                entity.VOCoverPath = await hashService.HashBytesAsync(voCover.Content);
            }
        }

        public override async Task<Domain.Entities.SerieVolume?> GetByIdAsync(UpdateSerieVolumeCommand request)
            => await volumeRepostory.GetByIdAsync(request.Id);
    }
}
