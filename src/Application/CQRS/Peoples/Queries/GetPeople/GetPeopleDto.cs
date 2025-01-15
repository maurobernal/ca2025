using ca.Domain.Entities;

namespace ca.Application.CQRS.Peoples.Queries.GetPeople;
public class GetPeopleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public bool Child { get; set; }

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<People, GetPeopleDto>();

    }

}
