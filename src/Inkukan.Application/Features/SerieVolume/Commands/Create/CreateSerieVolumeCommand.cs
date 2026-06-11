using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Inkukan.Application.Features.SerieVolume.Commands.Create
{
    public class CreateSerieVolumeCommand : IRequest<SerieVolumeDto>
    {
        public int VolumeNumber { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public IFormFile? VFCover { get; set; }
        public IFormFile? VOCover { get; set; }
        public DateTime VOParutionDate { get; set; }
        public DateTime VFParutionDate { get; set; }
        public int RecommendedAge { get; set; }
        public string? EANCode { get; set; }
        public string? PriceCode { get; set; }
        public Guid MangaSerieId { get; set; }
    }
}