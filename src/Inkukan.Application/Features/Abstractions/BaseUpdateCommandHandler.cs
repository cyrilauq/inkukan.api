using AutoMapper;
using FluentValidation;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Abstractions
{
    public class BaseUpdateCommandHandler<TCommand, TDto, TEntity>(IBaseRepository<TEntity> repository, IValidator<TCommand> validator, IMapper mapper) 
        : IRequestHandler<TCommand, TDto>, IValidatable<TCommand>
        where TCommand : class, IRequest<TDto>
        where TEntity : class, new()
        where TDto : class
    {
        public async Task<bool> EnsureIsValidAsync(TCommand value, CancellationToken cancellationToken)
        {
            if (await AlreadyExistsAsync(value, cancellationToken)) throw new ConflictException("Another entity with the same value already exists");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value, cancellationToken);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request, cancellationToken);
            TEntity entity = await GetByIdAsync(request, cancellationToken) ?? throw new EntityValidationException("A validation error occure", ["The asked resource doesn't exists"]);
            mapper.Map(request, entity);
            await BeforeUpdateAsync(request, entity, cancellationToken);
            TEntity result = await repository.UpdateAsync(entity, cancellationToken);
            return mapper.Map<TDto>(result);
        }

        public virtual Task<TEntity?> GetByIdAsync(TCommand request, CancellationToken cancellationToken)
            => Task.FromResult((TEntity?)null);

        public virtual Task BeforeUpdateAsync(TCommand request, TEntity enttiy, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public virtual Task<bool> AlreadyExistsAsync(TCommand request, CancellationToken cancellationToken)
            => Task.FromResult(false);
    }
}
