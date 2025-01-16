using ca.Domain.Entities.Common;

namespace ca.Domain.Entities;

public class Hobbie : EntityAuditableBase
{
    public string Name { get; set; } = string.Empty;
    public ICollection<People> Peoples { get; set; } = new HashSet<People>();
}
