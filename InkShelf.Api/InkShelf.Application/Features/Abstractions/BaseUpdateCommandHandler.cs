using AutoMapper;
using FluentValidation;
using InkShelf.Application.Interface;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using MediatR;

namespace InkShelf.Application.Features.Abstractions
{
    public class BaseUpdateCommandHandler<TCommand, TDto, TEntity>(IBaseRepository<TEntity> repository, IValidator<TCommand> validator, IMapper mapper) 
        : IRequestHandler<TCommand, TDto>, IValidatable<TCommand>
        where TCommand : class, IRequest<TDto>
        where TEntity : class
        where TDto : class
    {
        public async Task<bool> EnsureIsValidAsync(TCommand value)
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(value);
            if (validationResult.IsValid) return true;
            throw new EntityValidationException("A validation exception occured", validationResult.Errors.Select(e => e.ErrorMessage));
        }

        public async Task<TDto> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await EnsureIsValidAsync(request);
            TEntity entity = mapper.Map<TEntity>(request);
            TEntity result = await repository.UpdateAsync(entity, cancellationToken);
            return mapper.Map<TDto>(result);
        }

        public virtual Task<bool> IsUniqueAsync(TCommand request, CancellationToken cancellationToken)
            => Task.FromResult(true);
    }
}
