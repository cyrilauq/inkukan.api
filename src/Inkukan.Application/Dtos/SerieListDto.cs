namespace Inkukan.Application.Dtos;

public class SerieListDto
{
    public string SerieTitleVF { get; set; } = string.Empty;
    public string VFCoverPath { get; set; } = string.Empty;

    public int TotalVolumeCount { get; set; }
    public int VolumeOwnedCount { get; set; }
}
