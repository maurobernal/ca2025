using ca.Domain.Entities.Common;

namespace ca.Domain.Entities;

public class Country : EntityAuditableBase
{
    public Country()
    {
        Peoples = new HashSet<People>();
    }
    public string Name { get; set; } = string.Empty;
    public ICollection<People> Peoples { get; set; }  
}
