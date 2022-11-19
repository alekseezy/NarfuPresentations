using System.ComponentModel.DataAnnotations;

namespace NarfuPresentations.Shared.API.Client.API.Settings;

public record APISettings : IValidatableObject
{
    public string UserAgent { get; set; } = default!;
    public string VersionNeutralEndpoint { get; set; } = default!;
    public string VersionedEndpoint { get; set; } = default!;

    public bool UseHttps { get; set; } = true;
    public int Timeout { get; set; } = -1;

    public bool UseInsecureCertificate { get; set; }
    public bool UseCustomCN { get; set; } = false;
    public string CertificateCN { get; set; } = default!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(VersionNeutralEndpoint))
            yield return new ValidationResult($"{nameof(VersionNeutralEndpoint)} was not specified.");

        if (string.IsNullOrEmpty(VersionedEndpoint))
            yield return new ValidationResult($"{nameof(VersionedEndpoint)} was not specified.");
    }
}
