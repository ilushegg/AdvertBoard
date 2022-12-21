using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.Category
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(c => c.ParentCategory)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
