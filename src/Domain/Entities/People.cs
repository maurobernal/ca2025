using ca.Domain.Entities.Common;

namespace ca.Domain.Entities;
public class People : EntityAuditableBase
{ 
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public bool Child { get; set; }

    public int CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;

    public ICollection<Hobbie> Hobbies { get; set; } = new HashSet<Hobbie>();
}
