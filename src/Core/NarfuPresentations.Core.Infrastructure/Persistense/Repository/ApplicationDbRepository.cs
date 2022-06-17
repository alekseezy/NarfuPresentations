using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using Mapster;

using NarfuPresentations.Core.Application.Persistense;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using NarfuPresentations.Shared.Domain.Common.Contracts;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Repository;

public class ApplicationDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public ApplicationDbRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}