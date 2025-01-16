
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Peoples.Commands.UpdatePeople;
public class UpdatePeopleCommand : IRequest<int>
{
    public bool Child { get; set; }
    public int Id { get; set; }
}

public class UpdatePeopleCommandHandled( IApplicationDbContext context) : IRequestHandler<UpdatePeopleCommand, int>
{
    public async Task<int> Handle(UpdatePeopleCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Peoples
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();
        if(entity == null) throw new NotFoundException(request.Id.ToString(), "People");

        entity.Child = request.Child;


        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;


    }
}
