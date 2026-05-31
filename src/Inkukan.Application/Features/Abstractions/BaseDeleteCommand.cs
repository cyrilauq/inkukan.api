using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Abstractions
{
    public class BaseDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class BaseDeleteCommandHandler<TEntity, TCommand>(IBaseRepository<TEntity> repository)
        : IRequestHandler<TCommand>
        where TCommand : BaseDeleteCommand, IRequest
        where TEntity : class
    {
        public async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            TEntity entity = await repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new EntityNotFoundException($"No {typeof(TEntity).Name} with the id [{request.Id}] found");
            await repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
