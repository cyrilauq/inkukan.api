using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inkukan.Application.Dtos;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Inkukan.Application.Features.Abstractions;

public class BaseGetAllQueryHandler<TEntity, TDto, TCommand>(IBaseRepository<TEntity> repository, IMapper mapper)
    : IRequestHandler<TCommand, PaginatedDto<TDto>>
    where TEntity : class
    where TDto : class
    where TCommand : BaseGetAllQuery<TDto>
{
    protected IBaseRepository<TEntity> Repository => repository;
    protected IMapper Mapper => mapper;

    public async Task<PaginatedDto<TDto>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        IQueryable<TDto> query = GetQuery(request);

        query = ApplyFilters(query, request.Filters);
        query = SortQuery(query, request.Order, request.OrderBy);

        PaginatedDto<TDto> result = new()
        {
            TotalCount = await query.CountAsync(cancellationToken: cancellationToken),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Items = await query
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken)
        };

        return result;
    }

    private IQueryable<TDto> SortQuery(IQueryable<TDto> baseQuery, string? order, string? orderBy)
    {
        IQueryable<TDto> query = baseQuery;

        if (orderBy != null && order != null)
        {
            ParameterExpression orderByParam = Expression.Parameter(typeof(TDto), "p");
            MemberExpression orderByProp = Expression.Property(orderByParam, orderBy);
            LambdaExpression exp = Expression.Lambda(orderByProp, orderByParam);
            string method = order == "asc" ? "OrderBy" : "OrderByDescending";
            System.Type[] types = { query.ElementType, exp.Body.Type };
            MethodCallExpression mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

            query = query.Provider.CreateQuery<TDto>(mce);
        }
        return query;
    }

    private IQueryable<TDto> ApplyFilters(IQueryable<TDto> baseQuery, string[] filters)
    {
        IQueryable<TDto> query = baseQuery;

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

            ConstantExpression value;
            object convertedValue;
            System.Type targetType = propertyInfo.PropertyType;
            System.Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType == typeof(DateTime))
            {
                // 2. On parse en forçant l'ajustement UTC
                if (!DateTime.TryParse(searchQuery, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AdjustToUniversal | System.Globalization.DateTimeStyles.AssumeUniversal,
                    out DateTime parsedDate))
                {
                    continue;
                }

                // 3. On s'assure que le Kind est bien UTC
                convertedValue = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
            }
            else
            {
                convertedValue = Convert.ChangeType(searchQuery, targetType);
            }

            // 4. CRUCIAL : On passe targetType (DateTime?) explicitement à la constante
            // Cela évite que EF Core ne refasse un cast implicite qui perdrait le Kind UTC
            value = Expression.Constant(convertedValue, targetType);

            //try
            //{
            //    value = Expression.Constant(Convert.ChangeType(searchQuery, propertyInfo.PropertyType));
            //} 
            //catch(InvalidCastException) { }
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
                    // 1. On récupère la méthode "Contains" classique
                    MethodInfo? containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
                    // On récupère aussi la méthode "ToLower" pour les chaînes
                    MethodInfo? toLowerMethod = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);

                    if (containsMethod == null || toLowerMethod == null) continue;

                    // 2. Accès à la propriété
                    MemberExpression propertyAccess = Expression.Property(item, propertyName);

                    // 3. Transformation de la PROPRIÉTÉ en minuscules : x.Title.ToLower()
                    // On s'assure que c'est bien un string avant d'appeler ToLower
                    MethodCallExpression propertyToLower = Expression.Call(propertyAccess, toLowerMethod);

                    // 4. Transformation de la VALEUR RECHERCHÉE en minuscules
                    value = Expression.Constant(searchQuery.ToLower(), typeof(string));

                    // 5. Appel de .Contains sur la version en minuscules : x.Title.ToLower().Contains("valeur_en_minuscules")
                    MethodCallExpression containsCall = Expression.Call(propertyToLower, containsMethod, value);

                    // 6. Création de la Lambda avec vérification de nullité
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

    public virtual IQueryable<TDto> GetQuery(TCommand query)
    {
        return repository.GetQuery()
            .ProjectTo<TDto>(mapper.ConfigurationProvider);
    }
}

public class BaseGetAllQuery<TDto>
    : IRequest<PaginatedDto<TDto>>
    where TDto : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string[] Filters { get; set; } = [];

    public string? OrderBy { get; set; }
    public string? Order {  get; set; }
}
