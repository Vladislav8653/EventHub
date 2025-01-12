using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UsersConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);
    }
}