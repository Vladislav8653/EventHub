using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ParticipantsConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasMany(p => p.Events)
            .WithOne(ep => ep.Participant)
            .HasForeignKey(ep => ep.ParticipantId);

        builder.HasOne(p => p.User)
            .WithMany(u => u.Participants)
            .HasForeignKey(p => p.UserId);
        
        builder.Property(e => e.Id)
            .HasColumnName("ParticipantId") 
            .ValueGeneratedOnAdd(); 

        builder.Property(e => e.Name)
            .IsRequired() 
            .HasMaxLength(30) 
            .HasColumnName("Name"); 

        builder.Property(e => e.Surname)
            .IsRequired() 
            .HasMaxLength(30) 
            .HasColumnName("Surname"); 

        builder.Property(e => e.DateOfBirth)
            .IsRequired() 
            .HasColumnName("DateOfBirth"); 

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(254) 
            .HasColumnName("Email"); 
    }
}