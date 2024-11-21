﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataLayer.Data.Configurations;

public class ParticipantsConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasMany(p => p.Events)
            .WithOne(ep => ep.Participant)
            .HasForeignKey(ep => ep.ParticipantId);
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