﻿using System.ComponentModel.DataAnnotations;

namespace NarfuPresentations.Core.Infrastructure.Authentication.JWT.Settings;

public class JwtSettings : IValidatableObject
{
    public string Key { get; set; } = default!;
    public int TokenExpirationInMinutes { get; set; } = 60;
    public int RefreshTokenExpirationInMinutes { get; set; } = 1440;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Key))
            yield return new ValidationResult(
                "No Key defined in JwtSettings section configuration.",
                new[] { nameof(Key) });
    }
}
