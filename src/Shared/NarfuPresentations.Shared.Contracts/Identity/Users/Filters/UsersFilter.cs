using NarfuPresentations.Shared.Contracts.Common.Filters;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Filters;

public record UsersFilter : PaginationFilter
{
    public bool? IsActive { get; set; }
}
