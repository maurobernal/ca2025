namespace ca.Application.CQRS.Peoples.Commands.CreatePeople;
public class CreatePeopleValidator : AbstractValidator<CreatePeopleCommand>
{
    public CreatePeopleValidator()
    {
        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name empty")
                .MaximumLength(40).WithMessage("Name lenght is incorrect");

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate empty")
            .Must(Valid);

        RuleFor(x => x.CountryId)
            .NotEmpty().WithMessage("CountryId empty")
            .GreaterThan(0).WithMessage("CountryId must major 0");


        RuleFor(x => x.Hobbies)
    .NotEmpty().WithMessage("Hobbies empty");

    }

    private static bool Valid(DateOnly birthDate)
    {
        var now = DateTime.Now;
        int age = now.Year - birthDate.Year;
        if (now.Month >= birthDate.Month && now.Day >= birthDate.Day)
            age--;
        return age >= 18 && age <= 70;
    }
}
