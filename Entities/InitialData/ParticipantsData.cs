using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.InitialData;

public class ParticipantsData : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasData
        (
            new Participant()
            {
                DateOfBirth = new DateTime(2005, 14, 5),
                Email = "arefin.vlad@gmail.com",
                Events = [],
                Id = Guid.NewGuid(),
                Name = "Vladislav",
                Surname = "Arefin"
            },
            new Participant()
            {
                DateOfBirth = new DateTime(2006, 5, 26 ),
                Email = "egor.pomidor@gmail.com",
                Events = [],
                Id = Guid.NewGuid(),
                Name = "Egor",
                Surname = "Shcherbin"
            }
        );
    }
}