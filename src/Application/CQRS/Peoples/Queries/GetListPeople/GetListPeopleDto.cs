
using ca.Domain.Entities;

namespace ca.Application.CQRS.Peoples.Queries.GetListPeople;

public class GetListPeopleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping() => CreateMap<People, GetListPeopleDto>();

    }

}
