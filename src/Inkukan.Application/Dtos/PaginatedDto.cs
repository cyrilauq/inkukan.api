namespace Inkukan.Application.Dtos;

public class PaginatedDto<TDto>
    where TDto : class
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalCount { get; set; }
    public required IEnumerable<TDto> Items { get; set; }
}
