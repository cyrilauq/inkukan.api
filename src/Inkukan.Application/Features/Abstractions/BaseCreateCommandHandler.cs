using AutoMapper;
using FluentValidation;
using Inkukan.Application.Interface;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;

namespace Inkukan.Application.Features.Abstractions;

public class BaseCreateCommandHandler<TCommand, TDto, TEntity>(IBaseRepository<TEntity> repository, IValidator<TCommand> validator, IMapper mapper)
    : IRequestHandler<TCommand, TDto>, IValidatable<TCommand>
    where TCommand : class, IRequest<TDto>
    where TEntity : class, new()
    where TDto : class
{
    protected IBaseRepository<TEntity> Repository { get; set; } = repository;

    public virtual async Task<bool> EnsureIsValidAsync(TCommand value, CancellationToken cancellationToken)
    {
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value, cancellationToken);
        if (!validationResult.IsValid)
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        if (await AlreadyExistsAsync(value, cancellationToken)) throw new ConflictException("Another entity with the same value already exists");
        return true;
    }

    public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await EnsureIsValidAsync(request, cancellationToken);
        TEntity entity = mapper.Map<TEntity>(request);
        await BeforeCreateAsync(request, entity, cancellationToken);
        TEntity result = await Repository.UpdateAsync(entity, cancellationToken);
        return mapper.Map<TDto>(result);
    }

    public virtual Task BeforeCreateAsync(TCommand request, TEntity enttiy, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public virtual Task<bool> AlreadyExistsAsync(TCommand request, CancellationToken cancellationToken)
        => Task.FromResult(false);
}
