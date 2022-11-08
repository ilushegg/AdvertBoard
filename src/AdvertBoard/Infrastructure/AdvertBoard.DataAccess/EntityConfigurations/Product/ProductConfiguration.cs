using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <summary>
/// Конфигурация таблицы Products.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Domain.Product>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();
        
        builder.Property(b => b.Description).HasMaxLength(2000).IsRequired();

        builder.HasOne(s => s.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(s => s.CategoryId);

        builder.Property(b => b.DateTimeCreated);

        builder.Property(b => b.DateTimePublish);

        builder.Property(b => b.DateTimeUpdated);

        builder.HasMany(p => p.ProductImages)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId);

        builder.HasOne(s => s.User)
            .WithMany(p => p.Products)
            .HasForeignKey(s => s.UserId);
/*
        builder.HasMany(p => p.ShoppingCarts)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId);*/
    }
}