using AutoMapper;
using FluentValidation;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Abstractions
{
    public class BaseCreateCommandHandler<TCommand, TDto, TEntity>(IBaseRepository<TEntity> repository, IValidator<TCommand> validator, IMapper mapper)
        : IRequestHandler<TCommand, TDto>, IValidatable<TCommand>
        where TCommand : class, IRequest<TDto>
        where TEntity : class, new()
        where TDto : class
    {
        public virtual async Task<bool> EnsureIsValidAsync(TCommand value)
        {
            if (await AlreadyExistsAsync(value)) throw new ConflictException("Another entity with the same value already exists");
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);
            TEntity entity = mapper.Map<TEntity>(request);
            await BeforeCreateAsync(request, entity, cancellationToken);
            TEntity result = await repository.UpdateAsync(entity, cancellationToken);
            return mapper.Map<TDto>(result);
        }

        public virtual Task BeforeCreateAsync(TCommand request, TEntity enttiy, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public virtual Task<bool> AlreadyExistsAsync(TCommand request)
            => Task.FromResult(false);
    }
}
