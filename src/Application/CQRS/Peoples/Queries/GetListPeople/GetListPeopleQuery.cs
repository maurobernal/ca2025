using ca.Application.Common.Interfaces;


namespace ca.Application.CQRS.Peoples.Queries.GetListPeople;
public class GetListPeopleQuery : IRequest<List<GetListPeopleDto>>
{
    public string? Name { get; set; } = string.Empty;
    public int? Count { get; set; }
}
public class GetListPeopleQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetListPeopleQuery, List<GetListPeopleDto>>
{
    public async Task<List<GetListPeopleDto>> Handle(GetListPeopleQuery request, CancellationToken cancellationToken)
    {
        int.TryParse(request.Count.ToString(), out int count);

        request.Name ??= string.Empty;
        var res = await 
                context.Peoples
            .Where( w =>  request.Name == string.Empty ||  request.Name.Contains(w.Name))
            .ProjectTo<GetListPeopleDto>(mapper.ConfigurationProvider)
            .Take(count)
            .ToListAsync();
        return res;
    }
}
