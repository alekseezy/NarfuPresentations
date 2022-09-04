using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

using Ardalis.Specification;

using NarfuPresentations.Core.Application.Common.Exceptions;
using NarfuPresentations.Shared.Contracts.Common.Filters;

namespace NarfuPresentations.Core.Infrastructure.Common.Specification;

// TODO: Needs attention
// * Nullability of types
public static class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> SearchBy<T>(this ISpecificationBuilder<T> query,
        BaseFilter filter) =>
        query
            .SearchByKeyword(filter.Keyword)
            .AdvancedSearch(filter.AdvancedSearch)
            .AdvancedFilter(filter.AdvancedFilter);

    public static ISpecificationBuilder<T> PaginateBy<T>(this ISpecificationBuilder<T> query,
        PaginationFilter filter)
    {
        if (filter.PageNumber <= 0) filter.PageNumber = 1;

        if (filter.PageSize <= 0) filter.PageSize = 10;

        if (filter.PageNumber > 1) query = query.Skip((filter.PageNumber - 1) * filter.PageSize);

        return query
            .Take(filter.PageSize)
            .OrderBy(filter.OrderBy);
    }

    public static IOrderedSpecificationBuilder<T> SearchByKeyword<T>(
        this ISpecificationBuilder<T> specificationBuilder, string? keyword) =>
        specificationBuilder.AdvancedSearch(new Search { Keyword = keyword });

    public static IOrderedSpecificationBuilder<T> AdvancedSearch<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Search? search)
    {
        if (string.IsNullOrEmpty(search?.Keyword))
            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);

        if (search.Fields.Any())
            foreach (var field in search.Fields)
            {
                var paramExpr = Expression.Parameter(typeof(T));
                var propertyExpr = GetPropertyExpression(field, paramExpr);

                specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr,
                    search.Keyword);
            }
        else
            foreach (var property in typeof(T).GetProperties()
                         .Where(prop =>
                             (Nullable.GetUnderlyingType(prop.PropertyType) ??
                              prop.PropertyType) is
                             {
                                 IsEnum: false
                             } propertyType
                             && Type.GetTypeCode(propertyType) != TypeCode.Object))
            {
                var paramExpr = Expression.Parameter(typeof(T));
                var propertyExpr = Expression.Property(paramExpr, property);

                specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr,
                    search.Keyword);
            }

        return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
    }

    private static void AddSearchPropertyByKeyword<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Expression propertyExpr,
        ParameterExpression paramExpr,
        string keyword,
        string operatorSearch = FilterOperator.CONTAINS)
    {
        if (propertyExpr is not MemberExpression memberExpr ||
            memberExpr.Member is not PropertyInfo property)
            throw new ArgumentException("propertyExpr must be a property expression.",
                nameof(propertyExpr));

        var searchTerm = operatorSearch switch
        {
            FilterOperator.STARTSWITH => $"{keyword}%",
            FilterOperator.ENDSWITH => $"%{keyword}",
            FilterOperator.CONTAINS => $"%{keyword}%",
            _ => throw new ArgumentException("operatorSearch is not valid.", nameof(operatorSearch))
        };

        var selectorExpr =
            property.PropertyType == typeof(string)
                ? propertyExpr
                : Expression.Condition(
                    Expression.Equal(Expression.Convert(propertyExpr, typeof(object)),
                        Expression.Constant(null, typeof(object))),
                    Expression.Constant(null, typeof(string)),
                    Expression.Call(propertyExpr, "ToString", null, null));

        var selector = Expression.Lambda<Func<T, string>>(selectorExpr, paramExpr);

        ((List<SearchExpressionInfo<T>>)specificationBuilder.Specification.SearchCriterias)
            .Add(new SearchExpressionInfo<T>(selector, searchTerm));
    }

    public static IOrderedSpecificationBuilder<T> AdvancedFilter<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        Filter? filter)
    {
        if (filter is null)
            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);

        var parameter = Expression.Parameter(typeof(T));

        Expression binaryExpressionFilter;

        if (!string.IsNullOrEmpty(filter.Logic))
        {
            if (filter.Filters is null)
                throw new ApplicationLayerException(
                    "The Filters attribute is required when declaring a logic");
            binaryExpressionFilter =
                CreateFilterExpression(filter.Logic, filter.Filters, parameter);
        }
        else
        {
            var filterValid = GetValidFilter(filter);
            binaryExpressionFilter = CreateFilterExpression(filterValid.Field!,
                filterValid.Operator!,
                filterValid.Value, parameter);
        }

        ((List<WhereExpressionInfo<T>>)specificationBuilder.Specification.WhereExpressions)
            .Add(new WhereExpressionInfo<T>(
                Expression.Lambda<Func<T, bool>>(binaryExpressionFilter, parameter)));

        return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
    }

    private static Expression CreateFilterExpression(
        string logic,
        IEnumerable<Filter> filters,
        ParameterExpression parameter)
    {
        Expression? filterExpression = default;

        foreach (var filter in filters)
        {
            Expression bExpressionFilter;

            if (!string.IsNullOrEmpty(filter.Logic))
            {
                if (filter.Filters is null)
                    throw new ApplicationLayerException(
                        "The Filters attribute is required when declaring a logic");
                bExpressionFilter = CreateFilterExpression(filter.Logic, filter.Filters, parameter);
            }
            else
            {
                var filterValid = GetValidFilter(filter);
                bExpressionFilter = CreateFilterExpression(filterValid.Field!,
                    filterValid.Operator!,
                    filterValid.Value,
                    parameter);
            }

            filterExpression = filterExpression is null
                ? bExpressionFilter
                : CombineFilter(logic, filterExpression, bExpressionFilter);
        }

        return filterExpression!;
    }

    private static Expression CreateFilterExpression(
        string field,
        string filterOperator,
        object? value,
        ParameterExpression parameter)
    {
        var propertyExpression = GetPropertyExpression(field, parameter);
        var valueExpression = GetValueExpression(field, value, propertyExpression.Type);
        return CreateFilterExpression(propertyExpression, valueExpression, filterOperator);
    }

    private static Expression CreateFilterExpression(
        MemberExpression memberExpression,
        ConstantExpression constantExpression,
        string filterOperator) =>
        filterOperator switch
        {
            FilterOperator.EQ => Expression.Equal(memberExpression, constantExpression),
            FilterOperator.NEQ => Expression.NotEqual(memberExpression, constantExpression),
            FilterOperator.LT => Expression.LessThan(memberExpression, constantExpression),
            FilterOperator.LTE => Expression.LessThanOrEqual(memberExpression, constantExpression),
            FilterOperator.GT => Expression.GreaterThan(memberExpression, constantExpression),
            FilterOperator.GTE => Expression.GreaterThanOrEqual(memberExpression,
                constantExpression),
            FilterOperator.CONTAINS => Expression.Call(memberExpression, "Contains", null,
                constantExpression),
            FilterOperator.STARTSWITH => Expression.Call(memberExpression, "StartsWith", null,
                constantExpression),
            FilterOperator.ENDSWITH => Expression.Call(memberExpression, "EndsWith", null,
                constantExpression),
            _ => throw new ApplicationLayerException("Filter Operator is not valid.")
        };

#pragma warning disable CA2208
    private static Expression CombineFilter(
        string filterOperator,
        Expression bExpressionBase,
        Expression bExpression) =>
        filterOperator switch
        {
            FilterLogic.AND => Expression.And(bExpressionBase, bExpression),
            FilterLogic.OR => Expression.Or(bExpressionBase, bExpression),
            FilterLogic.XOR => Expression.ExclusiveOr(bExpressionBase, bExpression),
            _ => throw new ArgumentException("FilterLogic is not valid.", nameof(FilterLogic))
        };

#pragma warning restore CA2208

    private static MemberExpression GetPropertyExpression(
        string propertyName,
        ParameterExpression parameter)
    {
        var propertyExpression = propertyName.Split('.')
            .Aggregate<string?, Expression>(parameter,
                (current, member) => Expression.PropertyOrField(current, member));

        return (MemberExpression)propertyExpression;
    }

    private static string GetStringFromJsonElement(object value) =>
        ((JsonElement)value).GetString()!;

    private static ConstantExpression GetValueExpression(
        string field,
        object? value,
        Type propertyType)
    {
        if (value == null) return Expression.Constant(null, propertyType);

        if (propertyType.IsEnum)
        {
            var stringEnum = GetStringFromJsonElement(value);

            if (!Enum.TryParse(propertyType, stringEnum, true, out var parsedValue))
                throw new ApplicationLayerException($"Value {value} is not valid for {field}");

            return Expression.Constant(parsedValue, propertyType);
        }

        if (propertyType == typeof(Guid))
        {
            var stringGuid = GetStringFromJsonElement(value);

            if (!Guid.TryParse(stringGuid, out var parsedValue))
                throw new ApplicationLayerException($"Value {value} is not valid for {field}");

            return Expression.Constant(parsedValue, propertyType);
        }

        if (propertyType != typeof(string))
            return Expression.Constant(
                Convert.ChangeType(((JsonElement)value).GetRawText(), propertyType), propertyType);

        var text = GetStringFromJsonElement(value);

        return Expression.Constant(text, propertyType);
    }

    private static Filter GetValidFilter(Filter filter)
    {
        if (string.IsNullOrEmpty(filter.Field))
            throw new ApplicationLayerException(
                "The field attribute is required when declaring a filter");
        if (string.IsNullOrEmpty(filter.Operator))
            throw new ApplicationLayerException(
                "The Operator attribute is required when declaring a filter");
        return filter;
    }

    public static IOrderedSpecificationBuilder<T> OrderBy<T>(
        this ISpecificationBuilder<T> specificationBuilder,
        string[]? orderByFields)
    {
        if (orderByFields is null)
            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);

        foreach (var field in ParseOrderBy(orderByFields))
        {
            var paramExpr = Expression.Parameter(typeof(T));

            Expression propertyExpr = paramExpr;
            foreach (var member in field.Key.Split('.'))
                propertyExpr = Expression.PropertyOrField(propertyExpr, member);

            var keySelector = Expression.Lambda<Func<T, object?>>(
                Expression.Convert(propertyExpr, typeof(object)),
                paramExpr);

            ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                .Add(new OrderExpressionInfo<T>(keySelector, field.Value));
        }

        return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
    }

    private static Dictionary<string, OrderTypeEnum> ParseOrderBy(string[] orderByFields) =>
        new(orderByFields.Select((orderByField, index) =>
        {
            var fieldParts = orderByField.Split(' ');
            var field = fieldParts[0];
            var descending = fieldParts.Length > 1 &&
                             fieldParts[1].StartsWith("Desc", StringComparison.OrdinalIgnoreCase);
            var orderBy = index == 0
                ? descending
                    ? OrderTypeEnum.OrderByDescending
                    : OrderTypeEnum.OrderBy
                : descending
                    ? OrderTypeEnum.ThenByDescending
                    : OrderTypeEnum.ThenBy;

            return new KeyValuePair<string, OrderTypeEnum>(field, orderBy);
        }));
}
