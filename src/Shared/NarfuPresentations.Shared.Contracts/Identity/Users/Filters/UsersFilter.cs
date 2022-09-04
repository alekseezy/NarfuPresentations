using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.Common.Filters;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Filters;

[UsedImplicitly]
public record UsersFilter : PaginationFilter
{
    [UsedImplicitly] public bool? IsActive { get; set; }
}
