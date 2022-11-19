namespace NarfuPresentations.Core.Infrastructure.Identity.Settings;

public record PasswordSettings
{
    public int PasswordLength { get; set; } = 8;
    public bool RequireDigit { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
    public bool RequireNonAlphanumeric { get; set; } = true;
    public bool RequireUppercase { get; set; } = true;
}
