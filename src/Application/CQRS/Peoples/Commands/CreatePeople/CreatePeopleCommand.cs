using ca.Application.Common.Interfaces;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Peoples.Commands.CreatePeople;
public class CreatePeopleCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public bool Child { get; set; }

    private class Mapping : Profile
    {
        public Mapping() => CreateMap<CreatePeopleCommand, People>();
  
    }

}

public class CreatePeopleCommandHandler(IMapper mapper, IApplicationDbContext context) : IRequestHandler<CreatePeopleCommand, int>
{
    public async Task<int> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<People>(request);
        context.Peoples.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
