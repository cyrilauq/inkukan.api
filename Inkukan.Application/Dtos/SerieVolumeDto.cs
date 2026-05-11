namespace InkShelf.Application.Dtos
{
    public class SerieVolumeDto
    {
        public Guid Id { get; set; }

        public int VolumeNumber { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public ImageDto VFCoverPath { get; set; } = new();
        public ImageDto VOCoverPath { get; set; } = new();
        public DateTime VOParutionDate { get; set; }
        public string VOParutionCountry { get; set; } = string.Empty;
        public DateTime? VFParutionDate { get; set; }
        public string VFParutionCountry { get; set; } = string.Empty;
        public int RecommendedAge { get; set; }
        public string? EANCode { get; set; }
        public string? PriceCode { get; set; }

        public Guid MangaSerieId { get; set; }
        public MangaSerieDto MangaSerie { get; set; } = null!;
    }
}
