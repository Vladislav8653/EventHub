using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.InitialData;

public class EventsData : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData
        (
            new Event()
            {
                DateTime = new DateTimeOffset(2024, 07, 05, 20, 0, 0, TimeSpan.FromHours(2)),
                Description = "M72 Seasons World Tour",
                Id = Guid.NewGuid(),
                Image = [],
                MaxQuantityParticipant = 100000,
                Name = "Metallica Concert",
                Participants = [],
                Place = "Warsaw, PGE Narodowy"
            },
            new Event()
            {
                DateTime = new DateTimeOffset(2024, 07, 05, 20, 0, 0, TimeSpan.FromHours(3)),
                Description = "The coolest apples in the world are here",
                Id = Guid.NewGuid(),
                Image = [],
                MaxQuantityParticipant = 100,
                Name = "Exhibition of apples",
                Participants = [],
                Place = "Minsk, Komarovsky rinok"
            }
        );
    }
}