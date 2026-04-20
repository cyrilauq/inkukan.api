using InkShelf.Domain.Entities.Interfaces;

namespace InkShelf.Domain.Entities
{
    public class SerieVolume : ITrackableEntity, ILogicalDelete
    {
        public int VolumeNumber { get; set; }
        public string Synopsis { get; set; } = null!;
        public string? VFCoverPath { get; set; }
        public string? VOCoverPath { get; set; }
        public DateTime VOParutionDate { get; set; }
        public string VOParutionCountry { get; set; } = null!;
        public DateTime? VFParutionDate { get; set; }
        public string? VFParutionCountry { get; set; }
        public int RecommendedAge { get; set; }
        public string? EANCode { get; set; }
        public string? PriceCode { get; set; }

        public Guid MangaSerieId { get; set; }
        public MangaSerie MangaSerie { get; set; } = null!;

        #region ITrackableEntity

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        #endregion

        #region ILogicalDelete

        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; }
        #endregion
    }
}
