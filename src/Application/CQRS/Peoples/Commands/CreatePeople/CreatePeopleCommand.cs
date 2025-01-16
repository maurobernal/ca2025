﻿using ca.Application.Common.Interfaces;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Peoples.Commands.CreatePeople;
public class CreatePeopleCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public int CountryId { get; set; }
    public bool Child { get; set; }
    public List<int> Hobbies { get; set; } = new();

    public class Mapping : Profile
    {
        public Mapping() => CreateMap<CreatePeopleCommand, People>();
    }

}

    public class CreatePeopleCommandHandler(IMapper mapper, IApplicationDbContext context) : IRequestHandler<CreatePeopleCommand, int>
{
    public async Task<int> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
    {
        var country = await context.Countrys
            .AnyAsync(w => w.Id == request.CountryId);

        if (!country) throw new NotFoundException(request.CountryId.ToString(), "Country");

        
        // option1 - sintax method
        var listHobbiesBD = await context.Hobbies
            .Where (w => request.Hobbies.Contains(w.Id))
            .OrderBy(h=>h.Name)
            .Select(s => s.Id)
            .ToListAsync();

        if(listHobbiesBD.Count!= request.Hobbies.Count)
        {
            var falt= request.Hobbies.Except(listHobbiesBD).ToList();
            throw new NotFoundException(falt[0].ToString(), "Hobbie not found");
        }

        var entity = mapper.Map<People>(request);
        context.Peoples.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
