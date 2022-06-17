namespace NarfuPresentations.Shared.Domain.Common.Contracts;

public interface IAuditableEntity
{
    public DateTime Created { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }
}
