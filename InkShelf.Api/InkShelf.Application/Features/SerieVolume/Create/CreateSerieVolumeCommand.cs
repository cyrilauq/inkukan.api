using InkShelf.Application.Dtos;
using MediatR;

namespace InkShelf.Application.Features.SerieVolume.Create
{
    public class CreateSerieVolumeCommand : IRequest<SerieVolumeDto>
    {
        public int VolumeNumber { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string VFCoverPath { get; set; } = string.Empty;
        public string VOCoverPath { get; set; } = string.Empty;
        public DateTime VOParutionDate { get; set; }
        public string VOParutionCountry { get; set; } = string.Empty;
        public DateTime VFParutionDate { get; set; }
        public string VFParutionCountry { get; set; } = string.Empty;
        public int RecommendedAge { get; set; }
        public string? EANCode { get; set; }
        public string? PriceCode { get; set; }

        public Guid MangaSerieId { get; set; }
    }
}
