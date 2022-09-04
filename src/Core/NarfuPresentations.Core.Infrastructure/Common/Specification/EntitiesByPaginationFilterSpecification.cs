using NarfuPresentations.Shared.Contracts.Common.Filters;

namespace NarfuPresentations.Core.Infrastructure.Common.Specification;

public class EntitiesByPaginationFilterSpecification<T, TResult>
    : EntitiesByBaseFilterSpecification<T, TResult>
{
    public EntitiesByPaginationFilterSpecification(PaginationFilter filter)
        : base(filter)
    {
        Query.PaginateBy(filter);
    }
}

public class EntitiesByPaginationFilterSpecification<T> : EntitiesByBaseFilterSpecification<T>
{
    public EntitiesByPaginationFilterSpecification(PaginationFilter filter)
        : base(filter)
    {
        Query.PaginateBy(filter);
    }
}
