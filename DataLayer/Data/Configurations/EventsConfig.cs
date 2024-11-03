using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class EventsConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Participants)
            .WithOne(ep => ep.Event)
            .HasForeignKey(ep => ep.EventId);
        /*builder.HasData
        (
            new Event()
            {
                DateTime = new DateTimeOffset(2024, 07, 05, 20, 0, 0, new TimeSpan(2, 0, 0)),
                Description = "M72 Seasons World Tour",
                Id = Guid.NewGuid(),
                Image = [],
                MaxQuantityParticipant = 100000,
                Name = "Metallica Concert",
                Participants = [],
                Place = "Warsaw, PGE Narodowy",
                Category = new Category
                {
                    CategoryName = "Concerts",
                    Events =
                }
            },
            new Event()
            {
                DateTime = new DateTimeOffset(2024, 07, 05, 20, 0, 0, new TimeSpan(3, 0, 0)),
                Description = "The coolest apples in the world are here",
                Id = Guid.NewGuid(),
                Image = [],
                MaxQuantityParticipant = 100,
                Name = "Exhibition of apples",
                Participants = [],
                Place = "Minsk, Komarovsky rinok"
            }
        );*/
    }
}