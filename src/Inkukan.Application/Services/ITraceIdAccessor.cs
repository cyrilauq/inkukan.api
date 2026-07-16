namespace Inkukan.Application.Services;

public interface ITraceIdAccessor
{
    Guid TraceId { get; set; }
}
