﻿using Ardalis.Specification;

using NarfuPresentations.Shared.Domain.Common.Contracts;

namespace NarfuPresentations.Core.Application.Persistence;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T>
    where T : class, IAggregateRoot
{
}
