namespace Inkukan.Application.Features.Abstractions.Query;

public abstract class PaginatedQueryBase
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
