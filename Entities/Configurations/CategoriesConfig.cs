using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoriesConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(c => c.Events)
            .WithOne(e => e.Category);
        // .HasForeignKey(e => e.CategoryKey);
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