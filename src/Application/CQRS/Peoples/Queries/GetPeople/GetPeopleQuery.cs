
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Peoples.Queries.GetPeople;
public class GetPeopleQuery : IRequest<GetPeopleDto>
{
    internal int Id { get; set; }
    public void asignId(int id) => Id = id;
  
}


public class GetPeopleQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPeopleQuery, GetPeopleDto>
{
    public async Task<GetPeopleDto> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        var res = await context.Peoples
            .Where(x => x.Id == request.Id)
            .ProjectTo<GetPeopleDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (res == null) throw new NotFoundException(request.Id.ToString(),"Peoples");

        return res;
    }
}
