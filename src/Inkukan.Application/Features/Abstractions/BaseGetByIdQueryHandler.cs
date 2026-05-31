using AutoMapper;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities.Interfaces;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Abstractions
{
    public class BaseGetByIdQuery<TDto> : IRequest<TDto>
    {
        public Guid Id { get; set; }
    }

    public class BaseGetByIdQueryHandler<TDto, TEntity, TCommand>(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        : IRequestHandler<TCommand, TDto>
        where TEntity : class, ITrackableEntity
        where TDto : class
        where TCommand : BaseGetByIdQuery<TDto>, IRequest<TDto>
    {
        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            TEntity found = await FindByIdAsync(request.Id) ?? throw new EntityNotFoundException($"No entity found with the id [{request.Id}]");
            return mapper.Map<TDto>(found);
        }

        protected virtual Task<TEntity?> FindByIdAsync(Guid id)
            => baseRepository.GetByIdAsync(id);
    }
}
