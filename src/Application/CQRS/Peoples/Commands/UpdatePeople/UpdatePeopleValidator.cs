namespace ca.Application.CQRS.Peoples.Commands.UpdatePeople;
public class UpdatePeopleValidator : AbstractValidator<UpdatePeopleCommand>
{
    public UpdatePeopleValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
