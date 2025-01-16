using Domain.Models;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EventHubDbContext : DbContext
{
    public EventHubDbContext(DbContextOptions options) : base(options) {}
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriesConfig());
        modelBuilder.ApplyConfiguration(new UsersConfig());
        modelBuilder.ApplyConfiguration(new ParticipantsConfig());
        modelBuilder.ApplyConfiguration(new EventsConfig());
    }
}