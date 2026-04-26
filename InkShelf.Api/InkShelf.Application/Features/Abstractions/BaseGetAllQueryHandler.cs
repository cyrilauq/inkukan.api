using AutoMapper;
using AutoMapper.QueryableExtensions;
using InkShelf.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            IQueryable<TDto> query = GetQuery()
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize);

            query = ApplyFilters(query, request.Filters);
            query = SortQuery(query, request.Order, request.OrderBy);


            return await query.ToListAsync(cancellationToken);
        }

        private IQueryable<TDto> SortQuery(IQueryable<TDto> baseQuery, string? order, string? orderBy)
        {
            var query = baseQuery;

            if (orderBy != null && order != null)
            {
                var orderByParam = Expression.Parameter(typeof(TDto), "p");
                var orderByProp = Expression.Property(orderByParam, orderBy);
                var exp = Expression.Lambda(orderByProp, orderByParam);
                string method = order == "asc" ? "OrderBy" : "OrderByDescending";
                Type[] types = new Type[] { query.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

                query = query.Provider.CreateQuery<TDto>(mce);
            }
            return query;
        }

        private IQueryable<TDto> ApplyFilters(IQueryable<TDto> baseQuery, string[] filters)
        {
            var query = baseQuery;

            foreach (string filter in filters)
            {
                string[] filterComponents = filter.Split(' ');
                string propertyName = filterComponents[0];
                string filterMethod = filterComponents[1];
                string searchQuery = string.Join(' ', filterComponents.Skip(2)).Replace("'", string.Empty);

                ParameterExpression item = Expression.Parameter(typeof(TDto), typeof(TDto).Name);
                MemberExpression prop = Expression.Property(item, propertyName);
                PropertyInfo? propertyInfo = typeof(TDto).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null) continue;

                ConstantExpression value = Expression.Constant(string.Empty);
                try
                {
                    value = Expression.Constant(Convert.ChangeType(searchQuery, propertyInfo.PropertyType));
                } 
                catch(InvalidCastException) { }
                BinaryExpression equal;
                switch (filterMethod)
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
                    case "contains":
                        // 1. On récup!re les informations de la méthode "contains" de l'objet
                        System.Reflection.MethodInfo? containsMethod = propertyInfo.PropertyType.GetMethod("Contains", [typeof(string)]);
                        if (containsMethod == null) continue;

                        // 2. On récupérer la propriété "propertyName" de l'élément "item"
                        MemberExpression propertyAccess = Expression.Property(item, propertyName);

                        // 3. On défini la valeur qu'on recherche en lui spécifiant un type
                        value = Expression.Constant(searchQuery.ToLower(System.Globalization.CultureInfo.CurrentCulture), typeof(string));

                        // 4. On appelle la méthode "contains" sur la propriété de notre "item" et on lui passe le paramètre "value"
                        MethodCallExpression containsCall = Expression.Call(propertyAccess, containsMethod, value);

                        // 5. Création de la Lambda : on vérifie que la "propertyAccess" n'est pas null et qu'elle contient la valeur recherché
                        Expression<Func<TDto, bool>> containsLambdaExpression = Expression.Lambda<Func<TDto, bool>>(
                            Expression.AndAlso(
                                Expression.NotEqual(propertyAccess, Expression.Constant(null)),
                                containsCall
                            ),
                            item
                        );

                        query = query.Where(containsLambdaExpression);
                        continue;
                    case "eq":
                    default:
                        equal = Expression.Equal(prop, value);
                        break;
                }

                query = query.Where(Expression.Lambda<Func<TDto, bool>>(equal, item));
            }
            return query;
        }

        public virtual IQueryable<TDto> GetQuery()
        {
            return repository.GetQuery()
                .ProjectTo<TDto>(mapper.ConfigurationProvider);
        }
    }

    public class BaseGetAllQuery<TDto> : IRequest<IList<TDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[] Filters { get; set; } = [];

        public string? OrderBy { get; set; }
        public string? Order {  get; set; }
    }
}
