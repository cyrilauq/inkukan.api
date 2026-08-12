using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;

namespace Inkukan.Application.Features.SerieVolume.Queries.GetAllBySerie
{
    public class GetAllBySerieQuery : BaseGetAllQuery<SerieVolumeDto>
    {
        public Guid SerieId { get; set; }
    }
}
