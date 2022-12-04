using Ardalis.Specification;

using NarfuPresentations.Shared.Contracts.Common.Filters;

namespace NarfuPresentations.Core.Infrastructure.Common.Specification;

public class EntitiesByBaseFilterSpecification<T, TResult> : Specification<T, TResult>
{
    public EntitiesByBaseFilterSpecification(BaseFilter filter) => Query.SearchBy(filter);
}

public class EntitiesByBaseFilterSpecification<T> : Specification<T>
{
    public EntitiesByBaseFilterSpecification(BaseFilter filter) => Query.SearchBy(filter);
}
