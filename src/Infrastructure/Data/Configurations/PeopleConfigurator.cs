using ca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ca.Infrastructure.Data.Configurations;
public class PeopleConfigurator : IEntityTypeConfiguration<People>
{
    public void Configure(EntityTypeBuilder<People> builder)
    {
        builder.HasOne(s => s.Country)
            .WithMany(w => w.Peoples)
            .HasForeignKey(w => w.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(p => p.Hobbies)
            .WithMany(p => p.Peoples);
     


    }
}
