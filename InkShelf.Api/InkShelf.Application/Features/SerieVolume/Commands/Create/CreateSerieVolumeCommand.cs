using InkShelf.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InkShelf.Application.Features.SerieVolume.Commands.Create
{
    public class CreateSerieVolumeCommand : IRequest<SerieVolumeDto>
    {
        public int VolumeNumber { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public IFormFile? VFCover { get; set; }
        public IFormFile? VOCover { get; set; }
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