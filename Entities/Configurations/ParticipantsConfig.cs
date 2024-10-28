using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.Configurations;

public class ParticipantsConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasMany(p => p.Events)
            .WithOne(ep => ep.Participant);
        /*builder.HasData
        (
            new Participant()
            {
                DateOfBirth = new DateOnly(2005, 5, 14),
                Email = "arefin.vlad@gmail.com",
                Events = [],
                Id = Guid.NewGuid(),
                Name = "Vladislav",
                Surname = "Arefin"
            },
            new Participant()
            {
                DateOfBirth = new DateOnly(2006, 5, 26),
                Email = "egor.pomidor@gmail.com",
                Events = [],
                Id = Guid.NewGuid(),
                Name = "Egor",
                Surname = "Shcherbin"
            }
        );*/
    }
}