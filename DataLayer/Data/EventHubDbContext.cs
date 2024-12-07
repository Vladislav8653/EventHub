using DataLayer.Data.Configurations;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data;

public class EventHubDbContext : DbContext
{
    public EventHubDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriesConfig());
        modelBuilder.ApplyConfiguration(new UsersConfig());
        modelBuilder.ApplyConfiguration(new ParticipantsConfig());
        modelBuilder.ApplyConfiguration(new EventsConfig());
        modelBuilder.ApplyConfiguration(new EventParticipantConfig());
    }
    
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventParticipant> EventsParticipants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
}