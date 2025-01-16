
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Peoples.Commands.DeletePeople;
public class DeletePeopleCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeletePeopleCommandHandler(IApplicationDbContext context) : IRequestHandler<DeletePeopleCommand, int>
{
    public async Task<int> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Peoples
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();
        if (entity == null) throw new NotFoundException(request.Id.ToString(), "People");

        context.Peoples.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
