using Ardalis.Specification;

using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Core.Application.Common.Responses.Extensions;

public static class PaginationResponseExtensions
{
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<TSource,
        TDestination>(
        this IReadRepositoryBase<TSource> readRepository,
        ISpecification<TSource, TDestination> specification,
        int pageNumber, int pageSize,
        CancellationToken cancellationToken)
        where TSource : class
        where TDestination : class, IResponse
    {
        var list = await readRepository.ListAsync(specification, cancellationToken);
        var count = await readRepository.CountAsync(specification, cancellationToken);

        return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
    }
}
