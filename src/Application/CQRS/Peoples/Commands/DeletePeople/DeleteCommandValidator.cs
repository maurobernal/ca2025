namespace ca.Application.CQRS.Peoples.Commands.DeletePeople;
public class DeleteCommandValidator : AbstractValidator<DeletePeopleCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
