using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;

namespace InkShelf.Application.Features.SerieVolume.Queries.GetAllBySerie
{
    public class GetAllBySerieQuery : BaseGetAllQuery<SerieVolumeDto>
    {
        public Guid SerieId { get; set; }
    }
}
