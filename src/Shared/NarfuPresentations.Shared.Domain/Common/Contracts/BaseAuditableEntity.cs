namespace NarfuPresentations.Shared.Domain.Common.Contracts;

public abstract record BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity, ISoftDelete
    where TId : struct
{
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }

    public BaseAuditableEntity()
    {
        Created = DateTime.UtcNow;
        LastModified = DateTime.UtcNow;
    }
}
