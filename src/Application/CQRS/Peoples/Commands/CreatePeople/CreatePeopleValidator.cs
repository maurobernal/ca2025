namespace ca.Application.CQRS.Peoples.Commands.CreatePeople;
public class CreatePeopleValidator : AbstractValidator<CreatePeopleCommand>
{
    public CreatePeopleValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name empty")
                .MaximumLength(40).WithMessage("Name lenght is incorrect");

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate empty")
            //.Must( d => d.Year>=2020);
            .Must(Valid);

    }

    private bool Valid(DateOnly birthDate)
    {
        var now = DateTime.Now;
        int age = now.Year - birthDate.Year;
        if (now.Month >= birthDate.Month && now.Day >= birthDate.Day)
            age--;
        return age >= 18 && age <= 70;
    }
}
