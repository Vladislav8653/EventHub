﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Data.Configurations;

public class CategoriesConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id); 
        builder.HasMany(c => c.Events)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId);
        /*builder.HasData(
            new Category()
            {
                CategoryName = "Metal Concert",
                Events = [],
                Id = Guid.NewGuid()
            },
            new Category()
            {
                CategoryName = "Exhibition",
                Events = [],
                Id = Guid.NewGuid()
            }
        );*/
    }
}