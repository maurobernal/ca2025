namespace ca.Domain.Entities.Common;

public abstract class EntityAuditableBase : EntityBase
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
