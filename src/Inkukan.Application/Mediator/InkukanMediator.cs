using Inkukan.Application.Mediator.Abstractions;

namespace Inkukan.Application.Mediator
{
    public class InkukaMediator(IServiceProvider provider) : IInkukaMediator
    {
        private readonly IServiceProvider _serviceProvider = provider;

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            System.Type requestType = request.GetType();
            System.Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            object handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Handler not found for {requestType.Name}");

            return await ((dynamic)handler).Handle((dynamic)request, cancellationToken);
        }

        public async Task Send(IRequest request, CancellationToken cancellationToken = default)
        {
            System.Type requestType = request.GetType();
            System.Type handlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);
            object handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Handler not found for {requestType.Name}");

            await ((dynamic)handler).Handle((dynamic)request, cancellationToken);
        }
    }
}
