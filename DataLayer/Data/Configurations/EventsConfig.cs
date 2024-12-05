using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Data.Configurations;

public class EventsConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Participants)
            .WithOne(ep => ep.Event)
            .HasForeignKey(ep => ep.EventId);
        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId);

        builder.Property(e => e.Id)
            .HasColumnName("EventId")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.DateTime)
            .IsRequired();

        builder.Property(e => e.Place)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CategoryId)
            .IsRequired();

        builder.Property(e => e.MaxQuantityParticipant)
            .IsRequired();

        builder.Property(e => e.Image)
            .HasMaxLength(100);
    }
}