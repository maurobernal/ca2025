namespace ca.Domain.Entities;
public class People
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public bool Child { get; set; }
}
