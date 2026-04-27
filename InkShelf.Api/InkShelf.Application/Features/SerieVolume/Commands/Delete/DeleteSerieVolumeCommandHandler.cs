using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.SerieVolume.Commands.Delete
{
    public class DeleteSerieVolumeCommandHandler(ISerieVolumeRepository serieVolumeRepository)
        : BaseDeleteCommandHandler<Domain.Entities.SerieVolume, DeleteSerieVolumeCommand>(serieVolumeRepository)
    {
    }
}
