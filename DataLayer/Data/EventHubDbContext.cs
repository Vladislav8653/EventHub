using Entities.Configurations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriesConfig());
        modelBuilder.ApplyConfiguration(new ParticipantsConfig());
        modelBuilder.ApplyConfiguration(new EventsConfig());
        modelBuilder.ApplyConfiguration(new EventParticipantConfig());
    }
    
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventParticipant> EventsParticipants { get; set; }
    public DbSet<Category> Categories { get; set; }
}