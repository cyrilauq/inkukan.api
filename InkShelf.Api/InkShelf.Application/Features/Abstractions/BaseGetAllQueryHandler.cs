using AutoMapper;
using AutoMapper.QueryableExtensions;
using InkShelf.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InkShelf.Application.Features.Abstractions
{
    public class BaseGetAllQueryHandler<TEntity, TDto, TCommand>(IBaseRepository<TEntity> repository, IMapper mapper)
        : IRequestHandler<TCommand, IList<TDto>>
        where TEntity : class
        where TDto : class
        where TCommand : BaseGetAllQuery<TDto>
    {
        public async Task<IList<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            IQueryable<TDto> query = repository.GetQuery()
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TDto>(mapper.ConfigurationProvider);

            foreach(string filter in request.Filters)
            {
                string[] filterComponents = filter.Split(' ');
                string propertyName = filterComponents[0];
                string searchQuery = string.Join(' ', filterComponents.Skip(2)).Replace("'", "");
                
                var item = Expression.Parameter(typeof(TDto), typeof(TDto).Name);
                var prop = Expression.Property(item, propertyName);
                var propertyInfo = typeof(TDto).GetProperty(propertyName);
                
                if (propertyInfo == null) continue;

                var value = Expression.Constant(Convert.ChangeType(searchQuery, propertyInfo.PropertyType));
                BinaryExpression equal;
                switch(filterComponents[1])
                {
                    case "gte": // Greater than or equal
                        equal = Expression.GreaterThanOrEqual(prop, value);
                        break;
                    case "gt":  // Greater than
                        equal = Expression.GreaterThan(prop, value);
                        break;
                    case "lt": // Smaller than
                        equal = Expression.LessThan(prop, value);
                        break;
                    case "lte": // Smaller than or equal
                        equal = Expression.LessThanOrEqual(prop, value);
                        break;
                    case "eq":
                    default:
                        equal = Expression.Equal(prop, value);
                        break;
                }

                query = query.Where(Expression.Lambda<Func<TDto, bool>>(equal, item));
            }

            return await query.ToListAsync(cancellationToken);
        }
    }

    public class BaseGetAllQuery<TDto> : IRequest<IList<TDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[] Filters { get; set; } = [];
    }
}
