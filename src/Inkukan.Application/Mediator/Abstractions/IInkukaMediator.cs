namespace Inkukan.Application.Mediator.Abstractions
{
    public interface IInkukaMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken);
        Task Send(IRequest request, CancellationToken cancellationToken);
    }
}
