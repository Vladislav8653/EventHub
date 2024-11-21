using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Data.Configurations;

public class EventParticipantConfig : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.HasKey(ep => new { ep.EventId, ep.ParticipantId });
        builder.HasOne(ep => ep.Participant)
            .WithMany(p => p.Events)
            .HasForeignKey(ep => ep.ParticipantId);
        
        builder.HasOne(ep => ep.Event)
            .WithMany(e => e.Participants)
            .HasForeignKey(ep => ep.EventId);
    }
}