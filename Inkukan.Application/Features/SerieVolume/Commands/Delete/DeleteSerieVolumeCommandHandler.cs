using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.SerieVolume.Commands.Delete
{
    public class DeleteSerieVolumeCommandHandler(ISerieVolumeRepository serieVolumeRepository)
        : BaseDeleteCommandHandler<Domain.Entities.SerieVolume, DeleteSerieVolumeCommand>(serieVolumeRepository)
    {
    }
}
